jQuery(function($){

	if($('#smartwizard').length){

		if($(window).width()<=992){
			var txt;
			
			$('#smartwizard .nav-item ').each(function(){
				txt = $(this).find('small').text();
				console.log(txt + "br");
			});

			
		}

	}


});
