$.getScript('/scripts/zxcvbn.js').done(function() {
  $(function() {
    $('input[name=password]').keyup(function() {
      var username = $('input[name=username]').val();
      var email = $('input[name=email]').val();
      var passwordStrength = zxcvbn($(this).val(), [username, email]);
      console.log(passwordStrength.score);
    });
  });
});
