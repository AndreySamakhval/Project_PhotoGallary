$(document).ready(function () {
    PhotoGenre.GetPhotos();
});

PhotoGenre = {
    GetPhotos: function () {
        var id = $('.genreId').attr('id');
        $.ajax({
            type: 'GET',
            url: '/home/photos/' + id,
            asynch: true,
            success: function (output, status, xhr) {
                PhotoGenre.RenderPhotosDiv(output);
            },
            error: function () {
                alert('Error');
            }
        });
    },
    RenderPhotosDiv: function (photos) {
        $('#divPhotosTmpl').tmpl(photos).appendTo('#containerPhotosId');

    }
}