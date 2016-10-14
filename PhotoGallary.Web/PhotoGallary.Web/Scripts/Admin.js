$(document).ready(function () {
    roles.getRoles();
    $("#btnAddUser").click(function () {
        $('#divAddUser').toggle();
    });
    $("#btnAddOneUser").click(function () {
        users.addUser();
    }
       
        )
});
users = {
    addUser: function () {
        var name = $('#inputName').val();
        var pass = $('#inputPassword').val();
        var con_pass = $('#inputConfirmPassword').val();
        var role = $('#divRadio input:checked').val();

        if (pass != con_pass) {
            $('#errorPassword').text('Password and confirm password must match!')
            $('#inputPassword').val('');
            $('#inputConfirmPassword').val('');
            return;
        }
        else {
            $.ajax({
                type: 'POST',
                url: '/admin/createuser',
                data:({Name: name, Password:pass, Role:role }),
                asynch: true,
                success: function (output, status, xhr) {
                    if (output) {
                        users.addUserTable(name, role);
                        $('#resultCreateUser').text('User '+name+' successfully added!');
                        $('#divAddUser :input').val('');
                    }
                },
                error: function () {
                    alert('Error');
                }
            });

        }

    },
    addUserTable: function (name, role) {
        $(' <tr><td>' + name + '</td><td>' + role + '</td><td><button type="button" class="btn btn" id="btnEdit" userName="' + name + '">Edit</button></td><td><button type="button" class="btn" id="btnDelete" userName="' + name + '">Delete</button></td></tr>').appendTo('#tableUsers');
      
    }
}
roles = {
    getRoles: function () {
        $.ajax({
            type: 'GET',
            url: '/admin/getroles',
            asynch: true,
            success: function (output, status, xhr) {
                roles.renderRoles(output);
            },
            error: function () {
                alert('Error');
            }
        });

    },
    renderRoles: function (output) {
        $('#rolesTmpl').tmpl(output).appendTo('#divRadio');
        $('#divRadio input:first').attr("checked", "checked");
    }
}

