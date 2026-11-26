(() => {
  const list = document.getElementById('releases-list');
  const status = document.getElementById('release-status');
  if (!list || !status) return;

  const repo = 'goooglecrome/BoostPilot';
  const make = (tag, className, text) => {
    const el = document.createElement(tag);
    if (className) el.className = className;
    if (text !== undefined) el.textContent = text;
    return el;
  };
  const formatDate = (value) => {
    if (!value) return 'Date not listed';
    const date = new Date(value);
    return Number.isNaN(date.getTime()) ? 'Date not listed' :
      date.toLocaleDateString('en-AU', { day: 'numeric', month: 'long', year: 'numeric' });
  };
  const formatSize = (bytes) => {
    if (!Number.isFinite(bytes) || bytes <= 0) return '';
    return bytes >= 1024 * 1024 ? (bytes / (1024 * 1024)).toFixed(1) + ' MB' : Math.ceil(bytes / 1024) + ' KB';
  };

  function renderRelease(release, index) {
    const article = make('article', 'release-card');
    const top = make('div', 'release-card-header');
    const state = make('span', 'status-label ' + (release.prerelease ? 'status-preview' : 'status-stable'));
    state.append(make('i'));
    state.append(document.createTextNode(release.prerelease ? ' PRE-RELEASE' : (index === 0 ? ' LATEST STABLE' : ' STABLE RELEASE')));
    top.append(state, make('span', 'version-label', release.tag_name || 'Release'));
    article.append(top);
    article.append(make('h3', '', release.name || release.tag_name || 'BoostPilot release'));
    article.append(make('p', 'release-date', 'Published ' + formatDate(release.published_at || release.created_at)));

    if (release.body) {
      const details = make('details', 'release-changelog');
      const summary = make('summary', '', 'Release notes');
      const body = make('p', '', release.body.length > 1800 ? release.body.slice(0, 1800) + '…' : release.body);
      details.append(summary, body);
      article.append(details);
    }

    const assets = Array.isArray(release.assets) ? release.assets : [];
    const downloadArea = make('div', 'release-assets');
    const usableAssets = assets.filter(asset => /\.(exe|msi|zip)$/i.test(asset.name || ''));
    if (usableAssets.length) {
      usableAssets.forEach(asset => {
        const link = make('a', 'button button-primary asset-download');
        link.href = asset.browser_download_url;
        link.rel = 'noreferrer';
        link.append(document.createTextNode('Download ' + asset.name));
        const size = formatSize(asset.size);
        if (size) link.append(make('small', '', size));
        link.append(document.createTextNode(' ↓'));
        downloadArea.append(link);
      });
    } else {
      downloadArea.append(make('p', 'release-no-assets', 'No installer file is attached to this release.'));
    }
    const notesLink = make('a', 'release-notes');
    notesLink.href = release.html_url;
    notesLink.target = '_blank';
    notesLink.rel = 'noreferrer';
    notesLink.textContent = 'Open on GitHub ↗';
    downloadArea.append(notesLink);
    article.append(downloadArea);
    return article;
  }

  async function getAllReleases() {
    const all = [];
    for (let page = 1; page <= 10; page += 1) {
      const url = 'https://api.github.com/repos/' + repo + '/releases?per_page=100&page=' + page;
      const response = await fetch(url, { headers: { Accept: 'application/vnd.github+json' } });
      if (!response.ok) throw new Error('GitHub returned ' + response.status);
      const batch = await response.json();
      if (!Array.isArray(batch)) throw new Error('GitHub returned an unexpected response.');
      all.push(...batch);
      if (batch.length < 100) break;
    }
    return all.filter(release => !release.draft).sort((a, b) =>
      new Date(b.published_at || b.created_at) - new Date(a.published_at || a.created_at)
    );
  }

  getAllReleases().then(releases => {
    list.replaceChildren();
    if (!releases.length) {
      list.append(make('div', 'empty-releases', 'There are no public BoostPilot releases yet.'));
      status.textContent = 'The release history is currently empty.';
      return;
    }
    releases.forEach((release, index) => list.append(renderRelease(release, index)));
    status.textContent = releases.length + ' published release' + (releases.length === 1 ? '' : 's') + ' found. The list is ordered from newest to oldest.';
  }).catch(error => {
    status.textContent = 'Could not load the live GitHub release list right now (' + error.message + '). You can still open the official releases page below.';
    const fallback = make('article', 'release-card');
    fallback.append(make('div', 'release-card-header', 'LATEST CONFIRMED RELEASE'));
    fallback.append(make('h3', '', 'BoostPilot OG v1.3.0'));
    fallback.append(make('p', 'release-date', 'Published 25 November 2026'));
    const link = make('a', 'button button-primary', 'Download v1.3.0 ↓');
    link.href = 'https://github.com/goooglecrome/BoostPilot/releases/download/v1.3.0/BoostPilot-Setup.exe';
    fallback.append(link);
    list.replaceChildren(fallback);
  });
})();
