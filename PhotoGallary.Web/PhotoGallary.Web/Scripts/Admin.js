$(document).ready(function () {
    roles.getRoles();

    $("#btnAddUser").click(function () {
        $('#divAddUser').toggle();
    });
    $("#btnAddOneUser").click(function () {
        users.addUser();
    });
    $(".btnDelete").click(function () {
        users.dltUser($(this).attr('userName'));
    });
    $(".btnEdit").click(function () {
        users.editUser($(this).attr('userName'));
    });
    $("#btnAddRole").click(function () {
        roles.addRole();
    });
    $("#btnDeleteRole").click(function () {
        roles.RemoveRole();
    });    
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
                        // $('#divAddUser :input').val('');
                        $('#inputPassword').val('');
                        $('#inputConfirmPassword').val('');
                        $('#inputName').val('');
                    }
                },
                error: function () {
                    alert('Error');
                }
            });
        }
    },
    addUserTable: function (name, role) {
        $(' <tr id="'+name+'"><td>' + name + '</td><td>' + role + '</td><td><button type="button" class="btn btnEdit" userName="' + name + '">Edit</button></td><td><button type="button" class="btn btnDelete" userName="' + name + '">Delete</button></td></tr>').appendTo('#tableUsers');
      },

    dltUser:function(name){
        $.ajax({
            type: 'POST',
            url: '/admin/deleteuser/',   
            data:({Name: name}),
            asynch: true,
            success: function (output, status, xhr) {
                $('#' + name).remove();
            },
            error: function () {
                alert('Error');
            }
        });
    },
    editUser: function (name) {
        $('#divEditUser').toggle();
        //$('#divEditUser input').each(function () {
        //    var i = $(this).val();
        //    if (i == name)
        //        $(this).attr('checked', 'checked');
        //});
        $('#labelUserName').empty();
        $('#labelUserName').append(name);
        
        $("#btnEditRole").click(function () {
            var role = $('#divEditUser input:checked').val();
            if (role == null)
                return false;
            $.ajax({
                type: 'POST',
                url: '/admin/editrole/',
                data: ({ Name: name, Role:role }),
                asynch: true,
                success: function (output, status, xhr) {
                    $('#divEditUser').hide();
                },
                error: function () {
                    alert('Error');
                }
            });
        })

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

    addRole:function(){
        var rolename = $('#inputRoleName').val();
        if (rolename == null || rolename=='')
        {
            $('#pErrorRoleName').text('You must enter the name of the role!');
            return false;
        }
        $('#inputRoleName').val('');
        $.ajax({
            type: 'POST',
            url: '/admin/addrole',
            data: ({ RoleName: rolename }),
            asynch: true,
            success: function (output, status, xhr) {
                $('#pErrorRoleName').empty();
                $('#divCheckRole').empty();
                $('#rolesCheckTmpl').tmpl(output).appendTo('#divCheckRole');
            },
            error: function () {
                alert('Error');
            }
        });
    },
    RemoveRole:function(){
        var checkRole = $('.checkBoxRole input:checked').val();
        if (checkRole == null) {
            $('#pErrorRoleCheck').text('You must choose a role!');
            return false;
        }
        $.ajax({
            type: 'POST',
            url: '/admin/removerole',
            data: ({ RoleName: checkRole }),
            asynch: true,
            success: function (output, status, xhr) {
                $('#pErrorRoleCheck').empty();
                $('#divCheckRole').empty();
                $('#rolesCheckTmpl').tmpl(output).appendTo('#divCheckRole');
            },
            error: function () {
                alert('Error');
            }
        });
    },

    renderRoles: function (output) {
        $('#rolesTmpl').tmpl(output).appendTo('#divRadio');
        $('#rolesTmpl').tmpl(output).appendTo('#divRadioEdit');
        $('#rolesCheckTmpl').tmpl(output).appendTo('#divCheckRole');
        $('#divRadio input:first').attr("checked", "checked");
    }
}

