document.addEventListener('DOMContentLoaded', () => {

  // Get all "navbar-burger" elements
  const $navbarBurgers = Array.prototype.slice.call(document.querySelectorAll('.navbar-burger'), 0);

  // Check if there are any navbar burgers
  if ($navbarBurgers.length > 0) {

    // Add a click event on each of them
    $navbarBurgers.forEach(el => {
      el.addEventListener('click', () => {

        // Get the target from the "data-target" attribute
        const target = el.dataset.target;
        const $target = document.getElementById(target);

        // Toggle the "is-active" class on both the "navbar-burger" and the "navbar-menu"
        el.classList.toggle('is-active');
        $target.classList.toggle('is-active');

      });
    });
  }


  var acc = document.getElementsByClassName("accordion");
  var i;

  for (i = 0; i < acc.length; i++) {
    acc[i].addEventListener("click", function () {
      this.classList.toggle("active");
      var panel = this.nextElementSibling;
      if (panel.style.display === "block") {
        panel.style.display = "none";
      } else {
        panel.style.display = "block";
      }
    });
  }


  // show main content with addEventListener
  var menuListElements = document.getElementsByClassName("menu-list");
  for (var j = 0; j < menuListElements.length; j++) {
    var element = menuListElements[j];
    if (element.tagName === 'P') {
      element.addEventListener('click', (event) => {
        var id = event.target.innerText;
        var mainContent = document.getElementsByClassName("articles")[0].children;
        for (var k = 0; k < mainContent.length; k++) {
          mainContent[k].style.display = 'none';
        }
        document.getElementById(id).style.display = 'block';
      });
    } else if (element.tagName === 'UL') {
      for (let index = 0; index < element.children.length; index++) {
        const el = element.children[index];
        el.addEventListener('click', (event) => {
          var id = event.target.innerText;
          var mainContent = document.getElementsByClassName("articles")[0].children;
          for (var k = 0; k < mainContent.length; k++) {
            mainContent[k].style.display = 'none';
          }
          document.getElementById(id).style.display = 'block';
        });
      }
    }
  }

  // navigation
  let currentLocation = window.location;
  var hash = currentLocation.hash;
  if (hash !== "") {
    let className = hash.slice(1);
    let element = document.getElementsByClassName(className)[0];
    element.click();
  } else {
    if (currentLocation.pathname.indexOf("docs.html") !== -1) {
      var mainContent = document.getElementsByClassName("articles")[0].children;
      for (var k = 0; k < mainContent.length; k++) {
        mainContent[k].style.display = 'none';
      }
      document.getElementById("Main").style.display = "block";
    }
  }
});