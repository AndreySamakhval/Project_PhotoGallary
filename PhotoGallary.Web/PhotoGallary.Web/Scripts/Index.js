$(document).ready(function () {
    Photos.GetGenres();
});

Photos = {
    GetGenres: function () {
        $.ajax({
            type: 'GET',
            url: '/home/genres',
            asynch: true,
            success: function (output, status, xhr) {
                Photos.RenderGenresDiv(output);
            },
            error: function () {
                alert('Error');
            }
        });
    },
    RenderGenresDiv: function (tempGenres) {
        $('#divAddGenresTmpl').tmpl(tempGenres).appendTo('#containerIndexPage');
        $(".container_photo_genres").each(function () {
            var id = $(this).attr("id");
            Photos.GetPhotos(id);
        });
    },
    GetPhotos: function (id) {
        $.ajax({
            type: 'GET',
            url: '/home/lastphotos/' + id,
            asynch: true,
            success: function (output, status, xhr) {
                $('#divGenresTmpl').tmpl(output).appendTo('#IdPhotoDiv' + id);
            },
            error: function () {
                alert('Error');
            }
        });
    }
}