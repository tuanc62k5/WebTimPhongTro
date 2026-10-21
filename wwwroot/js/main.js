/* ==========================================================================
   Cornerstone — main.js (vanilla, no jQuery)
   One function per feature. Guard clauses. IntersectionObserver.
   Honours prefers-reduced-motion.
   ========================================================================== */
(function () {
  "use strict";

  var reduceMotion = window.matchMedia("(prefers-reduced-motion: reduce)").matches;

  /* ---- Sticky header shadow --------------------------------------------- */
  function initStickyHeader() {
    var header = document.querySelector(".site-header");
    if (!header) return;
    var onScroll = function () {
      header.classList.toggle("is-stuck", window.scrollY > 8);
    };
    onScroll();
    window.addEventListener("scroll", onScroll, { passive: true });
  }

  /* ---- Mobile navigation ------------------------------------------------- */
  function initMobileNav() {
    var toggle = document.querySelector(".nav-toggle");
    var panel = document.querySelector(".nav-panel");
    if (!toggle || !panel) return;

    var close = function () {
      toggle.setAttribute("aria-expanded", "false");
      panel.classList.remove("open");
    };
    var open = function () {
      toggle.setAttribute("aria-expanded", "true");
      panel.classList.add("open");
    };

    toggle.addEventListener("click", function () {
      var expanded = toggle.getAttribute("aria-expanded") === "true";
      expanded ? close() : open();
    });

    panel.addEventListener("click", function (e) {
      if (e.target.closest("a")) close();
    });

    document.addEventListener("keydown", function (e) {
      if (e.key === "Escape") close();
    });

    window.addEventListener("resize", function () {
      if (window.innerWidth >= 992) close();
    });
  }

  /* ---- Scroll reveal ----------------------------------------------------- */
  function initReveal() {
    var els = document.querySelectorAll(".reveal");
    if (!els.length) return;
    if (reduceMotion || !("IntersectionObserver" in window)) {
      els.forEach(function (el) { el.classList.add("in"); });
      return;
    }
    var io = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        if (entry.isIntersecting) {
          entry.target.classList.add("in");
          io.unobserve(entry.target);
        }
      });
    }, { threshold: 0.12, rootMargin: "0px 0px -8% 0px" });
    els.forEach(function (el) { io.observe(el); });
  }

  /* ---- Count-up stats ---------------------------------------------------- */
  function initCountUp() {
    var nums = document.querySelectorAll("[data-count]");
    if (!nums.length) return;

    var animate = function (el) {
      var target = parseFloat(el.getAttribute("data-count"));
      var decimals = (el.getAttribute("data-decimals") | 0);
      var dur = 1500;
      if (reduceMotion) { el.textContent = target.toFixed(decimals); return; }
      var start = null;
      var step = function (ts) {
        if (!start) start = ts;
        var p = Math.min((ts - start) / dur, 1);
        var eased = 1 - Math.pow(1 - p, 3);
        el.textContent = (target * eased).toFixed(decimals);
        if (p < 1) requestAnimationFrame(step);
        else el.textContent = target.toFixed(decimals);
      };
      requestAnimationFrame(step);
    };

    if (!("IntersectionObserver" in window)) {
      nums.forEach(animate);
      return;
    }
    var io = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        if (entry.isIntersecting) {
          animate(entry.target);
          io.unobserve(entry.target);
        }
      });
    }, { threshold: 0.5 });
    nums.forEach(function (n) { io.observe(n); });
  }

  /* ---- FAQ accordion ----------------------------------------------------- */
  function initFaq() {
    var items = document.querySelectorAll(".faq-item");
    if (!items.length) return;

    items.forEach(function (item) {
      var btn = item.querySelector(".faq-q");
      var ans = item.querySelector(".faq-a");
      if (!btn || !ans) return;

      btn.addEventListener("click", function () {
        var isOpen = item.classList.contains("open");
        // close siblings (single-open accordion)
        items.forEach(function (other) {
          if (other !== item) {
            other.classList.remove("open");
            other.querySelector(".faq-q").setAttribute("aria-expanded", "false");
            other.querySelector(".faq-a").style.maxHeight = null;
          }
        });
        if (isOpen) {
          item.classList.remove("open");
          btn.setAttribute("aria-expanded", "false");
          ans.style.maxHeight = null;
        } else {
          item.classList.add("open");
          btn.setAttribute("aria-expanded", "true");
          ans.style.maxHeight = ans.scrollHeight + "px";
        }
      });
    });

    window.addEventListener("resize", function () {
      var openItem = document.querySelector(".faq-item.open .faq-a");
      if (openItem) openItem.style.maxHeight = openItem.scrollHeight + "px";
    });
  }

  /* ---- Scrollspy for nav ------------------------------------------------- */
  function initScrollspy() {
    var links = document.querySelectorAll(".primary-nav a[data-spy]");
    if (!links.length || !("IntersectionObserver" in window)) return;
    var map = {};
    links.forEach(function (l) {
      var id = l.getAttribute("href");
      if (id && id.charAt(0) === "#") map[id.slice(1)] = l;
    });
    var sections = Object.keys(map).map(function (id) { return document.getElementById(id); }).filter(Boolean);
    if (!sections.length) return;

    var io = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        if (entry.isIntersecting) {
          links.forEach(function (l) { l.classList.remove("active"); });
          if (map[entry.target.id]) map[entry.target.id].classList.add("active");
        }
      });
    }, { rootMargin: "-45% 0px -50% 0px" });
    sections.forEach(function (s) { io.observe(s); });
  }

  /* ---- Rental analysis form validation ----------------------------------- */
  function initAnalysisForm() {
    var form = document.getElementById("analysis-form");
    if (!form) return;
    var success = form.querySelector(".form-success");

    var validators = {
      required: function (v) { return v.trim().length > 0; },
      email: function (v) { return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(v.trim()); }
    };

    var validateField = function (field) {
      var input = field.querySelector(".form-control, .form-select");
      if (!input) return true;
      var val = input.value;
      var ok = true;
      if (input.hasAttribute("required") && !validators.required(val)) ok = false;
      if (ok && input.type === "email" && !validators.email(val)) ok = false;
      field.classList.toggle("invalid", !ok);
      return ok;
    };

    form.querySelectorAll(".form-field").forEach(function (field) {
      var input = field.querySelector(".form-control, .form-select");
      if (!input) return;
      input.addEventListener("blur", function () { validateField(field); });
      input.addEventListener("input", function () {
        if (field.classList.contains("invalid")) validateField(field);
      });
    });

    form.addEventListener("submit", function (e) {
      e.preventDefault();
      var allOk = true;
      var firstInvalid = null;
      form.querySelectorAll(".form-field").forEach(function (field) {
        var ok = validateField(field);
        if (!ok && !firstInvalid) firstInvalid = field.querySelector(".form-control, .form-select");
        if (!ok) allOk = false;
      });
      if (!allOk) {
        if (firstInvalid) firstInvalid.focus();
        return;
      }
      form.querySelector(".analysis-card__actions") && form.querySelector(".analysis-card__actions").setAttribute("hidden", "");
      if (success) {
        success.classList.add("show");
        success.setAttribute("role", "status");
      }
      form.reset();
    });
  }

  /* ---- Property filtering (properties.html) ------------------------------ */
  function initPropertyFilter() {
    var grid = document.getElementById("property-grid");
    if (!grid) return;
    var cards = Array.prototype.slice.call(grid.querySelectorAll(".property-card"));
    var chips = document.querySelectorAll("[data-filter-chip]");
    var selects = document.querySelectorAll("[data-filter-select]");
    var countEl = document.getElementById("results-count");
    var noResults = document.getElementById("no-results");
    var resetBtn = document.getElementById("filter-reset");

    var state = { type: "all", beds: "any", price: "any", status: "any" };

    var matches = function (card) {
      if (state.type !== "all" && card.dataset.type !== state.type) return false;
      if (state.status !== "any" && card.dataset.status !== state.status) return false;
      if (state.beds !== "any" && parseInt(card.dataset.beds, 10) < parseInt(state.beds, 10)) return false;
      if (state.price !== "any") {
        var rent = parseInt(card.dataset.rent, 10);
        var parts = state.price.split("-");
        var min = parseInt(parts[0], 10);
        var max = parts[1] === "plus" ? Infinity : parseInt(parts[1], 10);
        if (rent < min || rent > max) return false;
      }
      return true;
    };

    var apply = function () {
      var shown = 0;
      cards.forEach(function (card) {
        var ok = matches(card);
        card.classList.toggle("hide", !ok);
        if (ok) shown++;
      });
      if (countEl) countEl.innerHTML = "Showing <b>" + shown + "</b> of " + cards.length + " properties";
      if (noResults) noResults.classList.toggle("show", shown === 0);
    };

    chips.forEach(function (chip) {
      chip.addEventListener("click", function () {
        chips.forEach(function (c) { c.classList.remove("active"); c.setAttribute("aria-pressed", "false"); });
        chip.classList.add("active");
        chip.setAttribute("aria-pressed", "true");
        state.type = chip.dataset.filterChip;
        apply();
      });
    });

    selects.forEach(function (sel) {
      sel.addEventListener("change", function () {
        state[sel.dataset.filterSelect] = sel.value;
        apply();
      });
    });

    if (resetBtn) {
      resetBtn.addEventListener("click", function () {
        state = { type: "all", beds: "any", price: "any", status: "any" };
        chips.forEach(function (c) {
          var on = c.dataset.filterChip === "all";
          c.classList.toggle("active", on);
          c.setAttribute("aria-pressed", on ? "true" : "false");
        });
        selects.forEach(function (s) { s.value = "any"; });
        apply();
      });
    }

    apply();
  }

  /* ---- Footer year ------------------------------------------------------- */
  function initYear() {
    var el = document.getElementById("year");
    if (el) el.textContent = new Date().getFullYear();
  }

  /* ---- Boot -------------------------------------------------------------- */
  document.addEventListener("DOMContentLoaded", function () {
    initStickyHeader();
    initMobileNav();
    initReveal();
    initCountUp();
    initFaq();
    initScrollspy();
    initAnalysisForm();
    initPropertyFilter();
    initYear();
  });
})();
