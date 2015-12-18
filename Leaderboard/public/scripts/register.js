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

    $('input').keyup(function() {
      var $password = $('input[name=password]');
      var $confirm = $('input[name=confirm]');
      if ($password.val() !== $confirm.val()) {
        $('input[name=confirm]').parent().addClass('is-invalid');
      } else {
        $('input[name=confirm]').parent().removeClass('is-invalid');
      }
    });
  });
});
