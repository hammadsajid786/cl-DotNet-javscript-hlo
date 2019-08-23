$(document).ready(function(){
    
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
      
    });

  
 /* User role */

    $('.user_role').click(function(){
        //alert("Hi");
        $('.user-dropdown').toggleClass('drop_menu');

    });


/* Toggle Side menu*/

  // $('.grow').click(function(){

  //   $('this').toggleClass('active');

  // });

});

