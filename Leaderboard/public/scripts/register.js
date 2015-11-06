$.getScript('/scripts/zxcvbn.js').done(function() {
  $(function() {
    $('input[name=password]').keyup(function() {
      var username = $('input[name=username]').val();
      var email = $('input[name=email]').val();
      var passwordStrength = zxcvbn($(this).val(), [username, email]);
      if (passwordStrength.score === 4) {
        $(this).parent().removeClass('is-invalid');
      } else {
        $(this).parent().addClass('is-invalid');
      }
    });

    $('input[name=confirm]').keyup(function() {
      if ($(this).val() !== $('input[name=password]').val()) {
        $(this).parent().addClass('is-invalid');
      } else {
        $(this).parent().removeClass('is-invalid');
      }
    });
  });
});
