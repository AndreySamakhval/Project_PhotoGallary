$(document).ready(function () {

    Genres.GetGenres();
    //$('#linkGenres').click(        
    //function () {
    //    if (!Genres.showGenres) {
    //        $('#navTabsGanres').show();
    //        Genres.showGenres = true;
    //    }
    //    else {
    //        $('#navTabsGanres').hide();
    //        Genres.showGenres = false;
    //    }    
    //});
    //$('.navBarLink').click(
    //    function () {
    //        showGenres = $(this).attr("id");
    //    });

});

Genres = {
   // showGenres:0,

    GetGenres: function () {
        $.ajax({
            type: 'GET',
            url: '/home/genres',
            asynch: true,
            success: function (output, status, xhr) {
                Genres.RenderGenresNav(output);
              //  Genres.RenderGenresDiv(output);
            },
            error: function () {
                alert('Error');
            }
        });
    },
    RenderGenresNav: function (tempGenres) {
        $('#navTabsGanres').empty();
        $('#genresTmpl').tmpl(tempGenres).appendTo('#navTabsGanres');

        $('.navBarMenu').each(function () {
            var idGenre = $(this).attr("id");
            var idPageGenre = $('.genreId').attr('id');
            if (idGenre == idPageGenre) {
                $(this).addClass('active');
            }
        });

    },
    //RenderGenresDiv: function (tempGenres) {
    //    $('#divAddGenresTmpl').tmpl(tempGenres).appendTo('#containerIndexPage');
    //    $(".container_photo_genres").each(function () {
    //        var id = $(this).attr("id");
    //        Genres.GetPhotos(id);
    //    });
    //},
    //GetPhotos: function (id) {
    //    $.ajax({
    //        type: 'GET',
    //        url: '/home/lastphotos/' + id,
    //        asynch: true,
    //        success: function (output, status, xhr) {
    //            $('#divGenresTmpl').tmpl(output).appendTo('#IdPhotoDiv'+ id);
    //        },
    //        error: function () {
    //            alert('Error');
    //        }
    //    });
    //}
}
