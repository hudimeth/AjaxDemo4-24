$(() => {
    const modal = new bootstrap.Modal($('.modal')[0]);

    function refreshTable() {
        $('tbody').html('');
        $.get('/home/getpeople', function (people) {
            people.forEach(function ({ id, firstName, lastName, age }) {
                $('tbody').append(`<tr>
                                        <td>${firstName}</td>
                                        <td>${lastName}</td>
                                        <td>${age}</td>
                                        <td>
                                            <button class='btn btn-warning edit' data-person-id='${id}'>Edit</button>
                                        </td>
                                        <td>
                                            <button class='btn btn-danger delete' data-person-id='${id}'>Delete</button>
                                        </td>
                                    </tr>`)
            })
        })
    }
    refreshTable();

    $('#add-person').on('click', function () {
        $('#firstName').val('');
        $('#lastName').val('');
        $('#age').val('');
        $('.modal-title').text('Add Person');
        $('#save-person').show();
        $('#update-person').hide();
        modal.show();
    });

    $('table').on('click', '.edit', function () {
        $('.modal-title').text('Edit Person');
        const button = $(this);
        const id = button.data('person-id');
        $.get('/home/getpersonbyid', { id }, function ({ firstName, lastName, age }) {
            $('#firstName').val(firstName);
            $('#lastName').val(lastName);
            $('#age').val(age);
            modal.show();
        });
        $('#update-person').show();
        $('#update-person').data('person-id', id); //for some odd reason it isnt working to set the data attribute of the button
        $('#save-person').hide();
        
    })

    $('#save-person').on('click', function () {
        const firstName = $('#firstName').val();
        const lastName = $('#lastName').val();
        const age = parseInt($('#age').val());

        $.post('/home/addperson', { firstName, lastName, age }, function () {
            refreshTable();
            modal.hide();
        })
    })

    $('table').on('click', '.delete', function () {
        const button = $(this);
        const id = button.data('person-id');
        $.post('/home/deleteperson', { id }, function () {
            refreshTable();
        })
    })

    $('#update-person').on('click', function () {
        const button = $(this);
        const id = button.data('person-id');//i cant run this because the data-attribute id was never set on the button
        const firstName = $('#firstName').val();
        const lastName = $('#lastName').val();
        const age = parseInt($('#age').val());

        $.post('/Home/EditPerson', { id, firstName, lastName, age }, function () {
            refreshTable();
            modal.hide();
        })
    })
});