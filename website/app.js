(() => {
  const toggle = document.querySelector('.menu-toggle');
  const nav = document.querySelector('.main-nav');

  // Keep the Privacy Policy discoverable from every published page.
  if (nav && !nav.querySelector('a[href="privacy-policy.html"]')) {
    const privacyNav = document.createElement('a');
    privacyNav.href = 'privacy-policy.html';
    privacyNav.textContent = 'Privacy Policy';
    nav.append(privacyNav);
  }

  // Keep the Terms & Conditions discoverable from every published page.
  if (nav && !nav.querySelector('a[href="terms.html"]')) {
    const termsNav = document.createElement('a');
    termsNav.href = 'terms.html';
    termsNav.textContent = 'Terms & Conditions';
    nav.append(termsNav);
  }

  const footerLinks = document.querySelector('.footer-links');
  if (footerLinks && !footerLinks.querySelector('a[href="privacy-policy.html"]')) {
    const privacyFooter = document.createElement('a');
    privacyFooter.href = 'privacy-policy.html';
    privacyFooter.textContent = 'Privacy Policy';
    footerLinks.append(privacyFooter);
  }

  if (footerLinks && !footerLinks.querySelector('a[href="terms.html"]')) {
    const termsFooter = document.createElement('a');
    termsFooter.href = 'terms.html';
    termsFooter.textContent = 'Terms & Conditions';
    footerLinks.append(termsFooter);
  }

  if (toggle && nav) {
    toggle.addEventListener('click', () => {
      const isOpen = nav.classList.toggle('open');
      toggle.setAttribute('aria-expanded', String(isOpen));
      toggle.setAttribute('aria-label', isOpen ? 'Close navigation' : 'Open navigation');
    });
    nav.querySelectorAll('a').forEach(link => link.addEventListener('click', () => {
      nav.classList.remove('open');
      toggle.setAttribute('aria-expanded', 'false');
      toggle.setAttribute('aria-label', 'Open navigation');
    }));
  }

  const year = document.getElementById('year');
  if (year) year.textContent = String(new Date().getFullYear());
})();
