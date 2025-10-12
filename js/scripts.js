/*!
* Start Bootstrap - Grayscale v7.0.4 (https://startbootstrap.com/theme/grayscale)
* Copyright 2013-2021 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-grayscale/blob/master/LICENSE)
*/
//
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    function capitalizeFirstLetter(str) {
        var s = String(str);
        if (s.length <= 0)
        {
            return s;
        }
        else
        {
            return s.charAt(0).toUpperCase() + s.slice(1);
        }
    }

    function lowerFirstLetter(str) {
        var s = String(str);
        if (s.length <= 0)
        {
            return s;
        }
        else
        {
            return str.charAt(0).toLowerCase() + str.slice(1);
        }
    }

    function createProjectCard (container, element, projectCounter) {
        const card = document.createElement('div');
        card.id = 'project' + projectCounter;
        
        
        var found_id = false;
        if (element.categories) 
        {
            element.categories.forEach(cat => {
                var formedcat = capitalizeFirstLetter(cat.toLowerCase());
                if (card.classList.contains(formedcat) == false)
                    {
                        card.className += ' ' + formedcat;
                        found_id=true;
                        // card.className += ' ' + cat
                    }
                });
            if (card.classList.length <= 0)
            {
                found_id = false;
                card.classList.add("undefined");
            }
            
            const select = document.getElementById("categorySelect");
            if (select) {
                if (card.classList.contains(capitalizeFirstLetter(select.value.toLowerCase())) == false && select.value != "View All") 
                {
                    var lowerSelect = select.value.toLowerCase();
                    if(!(lowerSelect == "undefined" && found_id==false))
                    {
                        return false;
                    }
                }
            }
        }
        
        card.className += ' col col-md-5 mb-3 mb-md-0 projectCardMain';
        var htmlStr = '<div id ="btn-'+projectCounter+ '" class="card py-4 h-100 projectCard border-dark" data-bs-toggle="modal" data-bs-target="#projectModal">';
        htmlStr += '<div class="card-body text-center">';

        //Title
        htmlStr += '<h5 class="card-title project-card-title">';
        htmlStr += element.title;
        htmlStr += '</h5>';

        //decription
        htmlStr += '<p class="card-text project-card-text">';
        htmlStr += element.description;
        htmlStr += '</p>';
        
        htmlStr += '<button/';// id="btn-';
        htmlStr += projectCounter;
        htmlStr += '" class="btn btn-primary project-button" data-bs-toggle="modal" data-bs-target="#projectModal">View Details</button>';

        htmlStr += '</div></div>';
        
        card.innerHTML = htmlStr;

        container.appendChild(card);

        const eleClicker = document.body.querySelector('#btn-'+projectCounter);
        
        if (eleClicker) {
            eleClicker.addEventListener('click',()=>{
                document.getElementById('projectModalTitle').innerText = element.title;
                document.getElementById('projectModalBody').style="";
                document.getElementById('projectModalBody').innerHTML = "";

                if (element.file) {
                    loadText("projectModalBody", element.file, false);
                }
                else {
                    var modelBodyHtml = '<p class="text-black">';
                    if (element.longDescription) {
                        modelBodyHtml += element.longDescription;
                    }
                    else {
                        modelBodyHtml += element.description;
                    }

                    modelBodyHtml += '<hr></p><div class="justify-content-center text-center col"><a class="btn btn-primary';
                    if (element.downloadPath)
                    {
                        modelBodyHtml += '" style="width:33%;" href="';
                        modelBodyHtml += element.downloadPath;
                        modelBodyHtml += '" download>Download</a>';
                    }
                    else
                    {
                        modelBodyHtml += ' inactive opacity-25" style="width:33%;" href="#">Download</a>';
                    }
                    modelBodyHtml += '<a class="btn btn-primary';

                    if (element.viewPath)
                    {
                        modelBodyHtml += '" style="width:33%; margin-left:5%;" href="';
                        modelBodyHtml += element.viewPath;
                        modelBodyHtml += '">View</a>';
                    }
                    else
                    {
                        modelBodyHtml += ' inactive opacity-25" style="width:33%; margin-left:5%;" href="#">View</a>';
                    }
                    modelBodyHtml += '</div>'
                    document.getElementById('projectModalBody').innerHTML = modelBodyHtml;
                }
            });
        }
        else
        {
            document.getElementById('projectModalTitle').innerText = "Error Loading data";
            document.getElementById('projectModalBody').innerHTML = "!!!!!!!!";
        }

        return true;
    }
    
    var createWorkElement = function (container, element)
    {
        const jobElement = document.createElement('div');
        jobElement.className = 'work-content justify-content-center text-center align-items-center card bg-transparent';
        jobElement.style = 'margin:2rem;';
        var htmlStr = "";
        
        if (element.longDescription)
        {
            htmlStr += '<div class="row gx-0 mb-5 mb-lg-0 justify-content-center card-body w-100 " style="margin-bottom:0% !important; padding-bottom:0% !important;">';

        }
        else
        {
            htmlStr += '<div class="row gx-0 mb-5 mb-lg-0 justify-content-center card-body w-100 " style="">';

        }
        if (element.titleImage)
        {
            htmlStr += '        <div class="col-lg-6" style="display:flex; justify-content:right;"><img class="img-fluid" src="'+element.titleImage+'" alt="'+element.company+'" /></div>';
            htmlStr += '            <div class="bg-black text-center align-items-center col-lg-6" style="display:flex; justify-content:left;">';
        }
        else
        {
            htmlStr += '            <div class="bg-black text-center align-items-center col-lg-6" style="display:flex; justify-content:center;">';
        }
        htmlStr += '                    <div class="work-text text-center text-lg-left project-text" style="padding:2rem;">';
        htmlStr += '                        <h4 class="text-white"><a href = "'+element.companyLink+'">'+element.company+'</a></h4>';
        htmlStr += '                        <br>';
        htmlStr += '                        <p class="mb-0 text-white-50">'+element.title+'</p>';
        htmlStr += '                        <br>';
        htmlStr += '                        <p class="mb-0 text-white-50">'+element.date+'</p>';
        htmlStr += '                        <br>';
        htmlStr += '                        <p class="mb-0 text-white-50">'+element.titleDescription+'';
        htmlStr += '                        </p>';
        htmlStr += '                        <hr class="d-none d-lg-block mb-0 ms-0" />';
        htmlStr += '                    </div>';
        htmlStr += '            </div>';
        // htmlStr += '    </div>';

        
        if (element.longDescription)
        {
            if (!element.titleImage)
            {
                htmlStr += '    </div>';
                htmlStr += '<div class="row gx-0 mb-5 mb-lg-0 justify-content-center card-body w-100 " style="margin-top:0% !important; padding-top:0% !important;">';
            }
            if (element.descriptionImage)
            {
                htmlStr += '            <div class="bg-black text-center align-items-center col-lg-6" style="display:flex; justify-content:right;">';
            }
            else
            {
                htmlStr += '            <div class="bg-black text-center align-items-center col-lg-6" style="display:flex; justify-content:center;">';
            }
            htmlStr += '                    <div class=" work-text text-center text-lg-left project-text">';
            htmlStr += '                        <p class="mb-0 text-white-50">'+element.longDescription+'</p>';
            htmlStr += '                        <hr class="d-none d-lg-block mb-0 ms-0" />';
            htmlStr += '                    </div>';
            htmlStr += '            </div>';
            if (element.descriptionImage)
            {
                htmlStr += '        <div class="col-lg-6" style="display:flex; justify-content:left;"><img class="img-fluid" src="'+element.descriptionImage+'" alt="'+element.company+'" /></div>';
                
            }

            if (!element.titleImage)
            {
                // htmlStr += '    </div>';
            }
        }
            htmlStr += '    </div>';

        //     htmlStr += '    <div class="row gx-0 mb-5 mb-lg-0  justify-content-center text-center align-items-center bg-black " style="">';
        //     // htmlStr += '    <div class="row gx-0 mb-5 mb-lg-0  justify-content-center text-center align-items-center bg-black "style="width:50%;">';
        //     if (element.descriptionImage) {
        //         htmlStr += '        <div class="col-lg-6"><img class="img-fluid" src="' + element.descriptionImage + '" alt="' + element.company + '" /></div>';
        //     }
        //     htmlStr += '        <div class="col-lg-6 order-lg-first">';
        //     htmlStr += '            <div class="bg-black text-center h-100 ">';
        //     // htmlStr += '                <div class="d-flex h-100">';
        //     htmlStr += '                    <div class="project-text w-100 text-center text-lg-right">';
        //     htmlStr += '                        <h4 class="text-white">Duties</h4>';
        //     htmlStr += '                        <p class="mb-0 text-white-50">';
        //     htmlStr += element.longDescription;
        //     htmlStr += '                            ';
        //     htmlStr += '                            </p>';
        //     htmlStr += '                        <hr class="d-none d-lg-block mb-0 me-0" />';
        //     // htmlStr += '                    </div>';
        //     htmlStr += '                </div>';
        //     htmlStr += '            </div>';
        //     htmlStr += '        </div>';
        //     htmlStr += '    </div>';
        // }


        jobElement.innerHTML = htmlStr;
        container.appendChild(jobElement);
    }



    var loadWorkJSON = function (sectionId, jsonFile)
    {
        fetch(jsonFile).then(
            response => response.json()
        ).then(
            data => 
            {
                const container = document.getElementById(sectionId);
                data.forEach(element => {
                    createWorkElement(container, element);
                });
            }
        ).catch(
            error => console.error("Error loading JSON file at section ID: ",error)
        );
        
    }



    async function collectProjectJsonCategories (jsonFile) {
        var items = Array();
        await fetch(jsonFile).then(
            response => response.json()
        ).then(
            data => 
            {
                var index = 1;
                data.forEach(element => {
                    
                    if (element.categories)
                    {
                        element.categories.forEach(category => {
                            if (items.includes(capitalizeFirstLetter(category.toLowerCase())) == false && category.length > 0 && category.toLowerCase() != "featured")
                            {
                                items[index] = (capitalizeFirstLetter(category.toLowerCase()));//category;
                                index += 1;
                            }
                        });
                        
                    }
                
                });
                items = items.sort();
                // items = ["View All", "Featured", ...items];
                items = ["Featured", "View All", ...items];
            }
        ).catch(
            error => console.error("Error loading JSON file at section ID: ",error)
        );


        return items;
        
    };

    async function loadProjectJsonDropdown (jsonFile) {
        var categories = await collectProjectJsonCategories(jsonFile);
        // Get reference to the select element
        const select = document.getElementById("categorySelect");

        // Populate dropdown
        for (var i = 0; i < categories.length; i++)
        {
            const option = document.createElement("option");
            option.classList.add('categoryOption');
            option.value = categories[i];      // the actual value
            option.textContent = capitalizeFirstLetter(categories[i]); // the text the user sees
            select.appendChild(option);
        };

         // Handle change event
        select.addEventListener("change", () => {
            loadProjectJSON("project-collection", "projects.json");
        });
    };


    function loadProjectJSON(sectionId, jsonFile)
    {
        var projectCounter = 0;
        var counts = 0;

        fetch(jsonFile).then(
            response => response.json()
        ).then(
            data => 
            {
                const container = document.getElementById(sectionId);
                container.innerHTML = "";
                data.forEach(element => {
                    projectCounter += 1;
                    if(createProjectCard(container, element, projectCounter))
                    {
                        counts += 1;
                    }
                
                });

                
            }
        ).catch(
            error => console.error("Error loading JSON file at section ID: ",error)
        );


        return (counts > 0);

    };





    var loadText = function(textSectionId, textFile, isMarkdown)
    {
        fetch(textFile).then(
            response => response.text()

        ).then(
            data => 
            {
                if(isMarkdown)
                {
                    document.getElementById(textSectionId).innerHTML=marked.parse(data);
                }
                else
                {
                    document.getElementById(textSectionId).innerHTML=data;
                }
            }
        ).catch(
            error => console.error("Error loading text file at section ID: ",error)
        );
    };


    // Navbar shrink function
    var navbarShrink = function () {
        const navbarCollapsible = document.body.querySelector('#mainNav');
        if (!navbarCollapsible) {
            return;
        }
        if (window.scrollY === 0) {
            navbarCollapsible.classList.remove('navbar-shrink')
        } else {
            navbarCollapsible.classList.add('navbar-shrink')
        }

    };

    document.addEventListener('scroll', navbarShrink);

     // Shrink the navbar 
     navbarShrink();

    // Activate Bootstrap scrollspy on the main nav element
    const mainNav = document.body.querySelector('#mainNav');
    if (mainNav) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#mainNav',
            offset: 74,
        });
    };

    // Collapse responsive navbar when toggler is visible
    const navbarToggler = document.body.querySelector('.navbar-toggler');
    const responsiveNavItems = [].slice.call(
        document.querySelectorAll('#navbarResponsive .nav-link')
    );
    responsiveNavItems.map(function (responsiveNavItem) {
        responsiveNavItem.addEventListener('click', () => {
            if (window.getComputedStyle(navbarToggler).display !== 'none') {
                navbarToggler.click();
            }
        });
    });

    loadText("about-text","about.txt", false);
    loadProjectJsonDropdown("projects.json");
    loadProjectJSON("project-collection", "projects.json");
    loadWorkJSON("work", "work.json")


    // document.addEventListener('DOMContentLoaded', function() {
    const modal = new bootstrap.Modal(document.getElementById('SITE_MSG'), {
  backdrop: false
});
    modal.show();
    // });
    

});