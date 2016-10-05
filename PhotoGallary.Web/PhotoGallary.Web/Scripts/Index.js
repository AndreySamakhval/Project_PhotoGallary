$(document).ready(function () {

    Genres.GetGenres();
  //  $('.ganre_link').click(Genres.GetPhotos());
    $('#linkGenres').click(
        
    function () {
        if (!Genres.showGenres) {
            $('#navTabsGanres').show();
            Genres.showGenres = true;
        }
        else {
            $('#navTabsGanres').hide();
            Genres.showGenres = false;
        }
    
    });

    //$(".ganre_link").live("click", function () {
    //    Genres.GetPhotos();
    //});
});

Genres = {
    showGenres: false,
    GetGenres: function () {

        $.ajax({
            type: 'GET',
            url: '/home/genres',
            asynch: true,
            success: function (output, status, xhr) {
                Genres.RenderGenresNav(output);
                Genres.RenderGenresDiv(output);

            },
            error: function () {
                alert('Error');
            }

        });
    },
    RenderGenresNav: function (tempGenres) {
        $('#navTabsGanres').empty();
        $('#genresTmpl').tmpl(tempGenres).appendTo('#navTabsGanres');
    },
    RenderGenresDiv: function (tempGenres) {
        $('#divAddGenresTmpl').tmpl(tempGenres).appendTo('#containerIndexPage');
        //Genres.GetPhotos();
        $(".container_photo_genres").
    },
    GetPhotos: function (id) {
        //var id = $(this).attr("id");
        $.ajax({
            type: 'GET',
            url: '/home/photos/' + id,
            asynch: true,
            success: function (output, status, xhr) {
                $('#divGenresTmpl').tmpl(output).appendTo('.photo3pt #' + id);
            },
            error: function () {
                alert('Error');
            }
        });
    }
}
