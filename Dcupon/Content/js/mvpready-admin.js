
var mvpready_admin = function () {

	"use strict"

	var initLayoutToggles = function () {
		$('.navbar-toggle, .mainnav-toggle').click (function (e) {
			$(this).toggleClass ('is-open')
		})
	}

	var initNoticeBar = function () {
		$('.noticebar > li > a').click (function (e) {
			if (mvpready_core.isLayoutCollapsed ()) {
				window.location = $(this).prop ('href')
			}
		})
	}

	return {
		init: function () {
			// Layouts
			mvpready_core.navEnhancedInit ()
			mvpready_core.navHoverInit ({ delay: { show: 250, hide: 350 } })      
			initLayoutToggles ()
			initNoticeBar ()

			// Components
			mvpready_core.initAccordions ()		
			mvpready_core.initFormValidation ()
			mvpready_core.initTooltips ()
			mvpready_core.initBackToTop ()		
			mvpready_core.initLightbox ()
		}
	}

}()

$(function () {
	mvpready_admin.init ()
});

$(document).ready(function () {
    $('.status-switch span').click(function () {
        if ($(this).attr('class') == 'active_deal') {

            $(this).addClass('inactive_deal').text('In Active');
        }
        else {
            $(this).text('Active');
            $(this).removeClass('inactive_deal').addClass('active_deal')

        }

    });


    $('body').on({
        click: function (e) {
            e.preventDefault();
            var id = $(this).attr('id');
            var action = '/Admin/CreateWebsites/' + id;
            $('form').attr('action', action);
            $('#Title').val($(this).parents('tr').find('td:eq(0)').text())
        }
    }, '.editwebsite')


    $('body').on({
        click: function (e) {
            e.preventDefault();
            var id = $(this).attr('id');
            var action = '/Admin/CreateCategory/' + id;
            $('form').attr('action', action);
            $('#Title').val($(this).parents('tr').find('td:eq(0)').text())
        }
    }, '.editCategory')

    $('body').on({
        click: function (e) {
            e.preventDefault();
            var id = $(this).attr('id');
            var cid = $(this).attr('data-dropval');
            var action = '/Admin/CreateSubCategory/' + id;
            $('form').attr('action', action);
            $('#Title').val($(this).parents('tr').find('td:eq(0)').text())
            $('#categoriesID').val(cid)
        }
    }, '.editSubCategory')

   // $('#Title').val('');
    $('#categoriesID').val('');

    $("form").validationEngine({
        scroll: false,
        promptPosition: "bottomLeft"
    });


    $('#RedirectType').change(function () {
        if ($(this).val() == '1') {
            $('#RedirectUrl').val('#')
        }
        else {
            $('#RedirectUrl').val('')
        }

    });

    $('.userError').fadeOut(0);




    $('.forgot-pass-submit').click(function () {
        if ($('#forgotemail').val() != '') {

            $.ajax({

                type: "POST",
                dataType: "text",
                url: "http://" + window.location.host + "/Admin/ForgotPassword?email=" + $('#forgotemail').val(),

                beforeSend: function () {
                    $('.forgot-pass-submit').text('Please wait...');
                    $('.forgot-pass-submit').attr('disabled', 'disabled');
                },

                success: function (data) {
                    var obj = jQuery.parseJSON(data);
                    if (obj.Authentication == 'UnSuccessfull') {
                        $('.forgot-pass-submit').removeAttr('disabled');
                        $('.userError').text(obj.Message);
                        $('.userError').fadeIn();
                        $('.forgot-pass-submit').text('Submit')
                    }

                    else {
                        $('.userError').fadeIn();                        
                        $('.userError').text(obj.Message);
                        $('.forgot-pass-submit').text('Submit');

                        setTimeout(function () {
                            $('#forgotpass').modal('hide')
                        }, 3500);
                      
                    }


                }
            })

        }

        else {
            $('#forgotemail').focus()

        }
    });














    $('.submit-pass').click(function () {

        if ($('#confirm-password').val() != '' && $('#confirmm').val() != '') {


            $.ajax({

                type: "POST",
                dataType: "text",
                url: "http://" + window.location.host + "/Admin/AdminPasswordReset?email=" + $('#email').val() + '&token=' + $('#token').val() + '&newpassword=' + $('#confirm-password').val(),

                beforeSend: function () {
                    $('.submit-pass').text('Please wait...');
                    $('.submit-pass').attr('disabled', 'disabled');
                },

                success: function (data) {
                    var obj = jQuery.parseJSON(data);
                    if (obj.Authentication == 'UnSuccessfull') {
                        $('.submit-pass').removeAttr('disabled');
                        $('.userError').text(obj.Message);
                        $('.userError').fadeIn();
                        $('.submit-pass').text('Submit')
                    }

                    else {
                        $('.userError').fadeIn();
                        $('.submit-pass').text('Submit')
                        $('.submit-pass').removeAttr('disabled');
                        $('.userError').text(obj.Message);
                    }


                }
            })


        }

        else {
            $('#confirm-password,#confirmm').focus();
        }

    });


    $('#UpdateSlider').parent().fadeOut();

    $('.edit-slider').click(function () {
        var imageid = $(this).attr('data-image');
        var description = $(this).attr('data-description');
        var link = $(this).attr('data-link');
        var updateid = $(this).attr('data-id');

        $('#UpdateSlider').parent().fadeIn();
        $('#updateid').val(updateid);
        $('#imageid').val(imageid);
        $('#description').val(description);
        $('#redirectlink').val(link)

    });


    $('.close').click(function () {

        $('.userError').fadeOut();

    });






});