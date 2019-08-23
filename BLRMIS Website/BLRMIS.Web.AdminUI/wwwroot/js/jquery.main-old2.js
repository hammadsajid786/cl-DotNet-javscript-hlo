$(document).ready(function(){
    $('.grow').click(function(e){
        e.preventDefault();
        $('sider-menu li').addClass('active');       
        $(this).addClass('active');
    });

    $('.a1').click(function(e){
        e.preventDefault();
        $(this).toggleClass('active');
    });
    $('.a2').click(function(e){
        e.preventDefault();
        $(this).toggleClass('active');
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


});

