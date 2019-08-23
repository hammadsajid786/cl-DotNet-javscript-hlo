$(document).ready(function () {

    $('.grow > a').click(function(e){
        e.preventDefault();
        //$('sider-menu li > a').removeClass('active')        
        $(this).parent().toggleClass('active');
    });

    $('.pretty-input').focus(function(){
        $(this).parents('.form-group').addClass('focused');
    });
    
    $('.pretty-input').blur(function(){
        var inputValue = $(this).val();
        if ( inputValue == "" ) {
            $(this).removeClass('filled');
            $(this).parents('.form-group').removeClass('focused');  
        } else {
            $(this).addClass('filled');
        }
    });

     $('.toggle-bars').click(function(){
        $('body').toggleClass('admin-sidebar');
        $('.sider-menu li').removeClass('active');
      
    });

  
 /* User role */

    $('.user_role').click(function(){
        
        $('.user-dropdown').toggleClass('drop_menu');

    });


/* Toggle Side menu*/
$('.a1').click(function(e){
        e.preventDefault();
        $(this).toggleClass('active');
    });
    $('.a2').click(function(e){
        e.preventDefault();
        $(this).toggleClass('active');
    });

    $("input[type=text]").on("keyup", function (evt) {
        if (evt.keyCode == "13")
            $("#complaintSearchButton").click();
    });

});


$(document).mouseup(function (e) {
    const $menu = $('.user-dropdown');
    if (!$menu.is(e.target) // if the target of the click isn't the container...
        && $menu.has(e.target).length === 0) // ... nor a descendant of the container
    {
        $menu.removeClass('drop_menu');
    }
});

$('.sorting').click(function (e) {

    e.preventDefault();

    if ($(this).children().children('.a1').hasClass('active')) {
        $(this).children().children('.a1').removeClass('active');
        $(this).children().children('.a2').addClass('active');
    }
    else if ($(this).children().children('.a2').hasClass('active')) {
        $(this).children().children('.a1').addClass('active');
        $(this).children().children('.a2').removeClass('active');
    }
    else {
        $('.sorting i').removeClass('active');
        $(this).children().children('.a1').addClass('active');
        $(this).children().children('.a2').removeClass('active');
    }

});
