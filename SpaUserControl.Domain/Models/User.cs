using System;
using SpaUserControl.Common.Resources;
using SpaUserControl.Common.Validation;

namespace SpaUserControl.Domain.Models
{
    public class User
    {
        #region Constructor
        protected User() { }

        public User(String name, String email) // TODO: Validate - Exception
        {
            this.Id = Guid.NewGuid();

            EmailAssertionConcern.AssertIsValid(email);
            this.ValidateName(name);

            this.Name = name;
            this.Email = email;
        } 
        #endregion

        #region Properties
        public Guid Id { get; private set; }

        public String Name { get; private set; }

        public String Email { get; private set; }

        public String Password { get; private set; } 
        #endregion

        #region Methods
        public void SetPassword(String password, String confirmPassword)
        {
            AssertionConcern.AssertArgumentNotEmpty(password, Errors.InvalidPassword);
            AssertionConcern.AssertArgumentNotEmpty(confirmPassword, Errors.InvalidPasswordConfirmation);
            AssertionConcern.AssertArgumentEquals(password, confirmPassword, Errors.PasswordDoNotMatch);
            AssertionConcern.AssertArgumentLength(password, 8, 20, Errors.PasswordIntervalCaractere);

            this.Password = PasswordAssertionConcern.Encrypt(password);
        }

        public String ResetPassword()
        {
            String password = Guid.NewGuid().ToString().Substring(0, 8);
            this.Password = PasswordAssertionConcern.Encrypt(password);

            return password;
        }

        public void ChanceName(String name)
        {
            this.ValidateName(name);
            this.Name = name;
        }

        private void ValidateName(String name)
        {
            AssertionConcern.AssertArgumentLength(name, 3, 150, Errors.InvalidUserName);
        }
        #endregion
    }
}