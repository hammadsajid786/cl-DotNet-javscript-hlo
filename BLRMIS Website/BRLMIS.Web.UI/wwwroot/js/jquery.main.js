$(document).ready(function(){
    /* Toggle arrow table*/
    $('.a1').click(function(e){
        e.preventDefault();
        $(this).toggleClass('active');
    });
    $('.a2').click(function(e){
        e.preventDefault();
        $(this).toggleClass('active');
    });

    $(".search-text").click(function(){
          $(this).parents('.form-inline').toggleClass("active");
    $(".search-fields").toggleClass('display-none');
        });

    /* Scroll  */
    // $(".boxscroll").niceScroll();
    // $("#boxscroll").niceScroll();

    $('.round-check input:checkbox').change(function(){
        if($(this).is(":checked")) {
            $(this).closest('li.list-check').addClass("active");
        } else {
            $(this).closest('li.list-check').removeClass("active");
        }
    });
    $('.navbar-toggler').click(function(e){
        $('body').toggleClass('remove-scroll');
    });
});
   
    
