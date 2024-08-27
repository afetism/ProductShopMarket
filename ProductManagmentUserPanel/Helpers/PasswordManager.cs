using System.Security.Cryptography;
using System.Text;

 namespace ProductManagmentUserPanel.Helpers;

    class PasswordManager
    {
	    public byte[] HashPassword(string password)
	    {
		   using (var sha256 = SHA256.Create())
		   {
			return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
		   }
	     }

	
	    public bool VerifyPassword(byte[] storedHash, string inputPassword)
	{
		
		  byte[] inputHash = HashPassword(inputPassword);

		
		   return storedHash.SequenceEqual(inputHash);
	    }



}
