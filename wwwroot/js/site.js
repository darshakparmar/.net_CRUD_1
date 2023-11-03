// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

<script>
console.log("DSfa");
    $(document).ready(function () {
        console.log("hiiiiii");
    // Attach an event listener to the input field
    $('#phoneNumber').on('input', function () {
            var value = $(this).val();
    var numericValue = value.replace(/[^0-9]/g, ''); // Remove non-numeric characters
    $(this).val(numericValue); // Update the input field with the numeric value
    console.log("byee");
        });
    });
</script>
