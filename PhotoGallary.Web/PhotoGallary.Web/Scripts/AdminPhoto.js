$(document).ready(function () {
    manage.getGenres();
    $("#btnAddGenre").click(function () {
        manage.addGenre();
    });

});
manage = {
    getGenres: function () {
        $.ajax({
            type: 'GET',
            url: '/home/genres',           
            asynch: true,
            success: function (output, status, xhr) {
                manage.renderGenres(output);
                $(".btnDeleteRole").click(function () {
                    manage.removeGenre($(this).attr('id'));
                });
            },
            error: function () {
                alert('Error');
            }
        });

    },
    addGenre: function () {
        var name = $('#inputNewGenre').val();
        if (name == null || name == '') {
            $('#pErrorGenreName').text('You must enter the name of new genre!');
            return false;
        }
        $('#inputNewGenre').val('');
        $.ajax({
            type: 'POST',
            url: '/home/addgenre',
            data: ({ Name: name }),
            asynch: true,
            success: function (output, status, xhr) {
                if (output!=0) {
                    $('<tr><td>'+name+'</td><td><button type="button" class="btn btn-danger btnDeleteRole" id="'+output+'">Delete</button></td></tr>').appendTo('#tableGenre');
                }
                else {
                    $('#pErrorGenreName').text('Genre '+name+' already exists!');
                }
            },
            error: function () {
                alert('Error');
            }
        });
    },

    removeGenre:function(id){      
        $('#tr'+ id).remove();

        $.ajax({
            type: 'GET',
            url: '/home/removegenre/'+id,            
            asynch: true,
            success: function (output, status, xhr) {
                $('#tr' + id).remove();
            },
            error: function () {
                alert('Error');
            }
        });
    },

    renderGenres: function (output) {
        $('')
        $('#showGenreTmpl').tmpl(output).appendTo('#tableGenre');
    }
}