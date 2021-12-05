//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    
    public partial class UserProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserProfile()
        {
            this.Comments = new HashSet<Comments>();
            this.Uploads = new HashSet<Image>();
            this.Followers = new HashSet<UserProfile>();
            this.Follows = new HashSet<UserProfile>();
            this.Like = new HashSet<Image>();
        }
    
        public long usrId { get; set; }
        public string loginName { get; set; }
        public string enPassword { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string language { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    	/// <summary>
        /// Relationship Name (Foreign Key in ER-Model): 
    	/// FK_User_Comm
        /// </summary>
        public virtual ICollection<Comments> Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    	/// <summary>
        /// Relationship Name (Foreign Key in ER-Model): 
    	///FK_User_Img
        /// </summary>
        public virtual ICollection<Image> Uploads { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    	/// <summary>
        /// Relationship Name (Foreign Key in ER-Model): 
    	/// Follow
        /// </summary>
        public virtual ICollection<UserProfile> Followers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    	/// <summary>
        /// Relationship Name (Foreign Key in ER-Model): 
    	/// Follow
        /// </summary>
        public virtual ICollection<UserProfile> Follows { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    	/// <summary>
        /// Relationship Name (Foreign Key in ER-Model): 
    	/// Likes
        /// </summary>
        public virtual ICollection<Image> Like { get; set; }
    
    	/// <summary>
    	/// A hash code for this instance, suitable for use in hashing algorithms and data structures 
    	/// like a hash table. It uses the Josh Bloch implementation from "Effective Java"
        /// Primary key of entity is not included in the hash calculation to avoid errors
    	/// with Entity Framework creation of key values.
    	/// </summary>
    	/// <returns>
    	/// Returns a hash code for this instance.
    	/// </returns>
    	public override int GetHashCode()
    	{
    	    unchecked
    	    {
    			int multiplier = 31;
    			int hash = GetType().GetHashCode();
    
    			hash = hash * multiplier + (loginName == null ? 0 : loginName.GetHashCode());
    			hash = hash * multiplier + (enPassword == null ? 0 : enPassword.GetHashCode());
    			hash = hash * multiplier + (firstName == null ? 0 : firstName.GetHashCode());
    			hash = hash * multiplier + (lastName == null ? 0 : lastName.GetHashCode());
    			hash = hash * multiplier + (email == null ? 0 : email.GetHashCode());
    			hash = hash * multiplier + (language == null ? 0 : language.GetHashCode());
    
    			return hash;
    	    }
    
    	}
        
        /// <summary>
        /// Compare this object against another instance using a value approach (field-by-field) 
        /// </summary>
        /// <remarks>See http://www.loganfranken.com/blog/687/overriding-equals-in-c-part-1/ for detailed info </remarks>
    	public override bool Equals(object obj)
    	{
    
            if (ReferenceEquals(null, obj)) return false;        // Is Null?
            if (ReferenceEquals(this, obj)) return true;         // Is same object?
            if (obj.GetType() != this.GetType()) return false;   // Is same type? 
    
            UserProfile target = obj as UserProfile;
    
    		return true
               &&  (this.usrId == target.usrId )       
               &&  (this.loginName == target.loginName )       
               &&  (this.enPassword == target.enPassword )       
               &&  (this.firstName == target.firstName )       
               &&  (this.lastName == target.lastName )       
               &&  (this.email == target.email )       
               &&  (this.language == target.language )       
               ;
    
        }
    
    
    	public static bool operator ==(UserProfile  objA, UserProfile  objB)
        {
            // Check if the objets are the same UserProfile entity
            if(Object.ReferenceEquals(objA, objB))
                return true;
      
            return objA.Equals(objB);
    }
    
    
    	public static bool operator !=(UserProfile  objA, UserProfile  objB)
        {
            return !(objA == objB);
        }
    
    
        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
    	public override String ToString()
    	{
    	    StringBuilder strUserProfile = new StringBuilder();
    
    		strUserProfile.Append("[ ");
           strUserProfile.Append(" usrId = " + usrId + " | " );       
           strUserProfile.Append(" loginName = " + loginName + " | " );       
           strUserProfile.Append(" enPassword = " + enPassword + " | " );       
           strUserProfile.Append(" firstName = " + firstName + " | " );       
           strUserProfile.Append(" lastName = " + lastName + " | " );       
           strUserProfile.Append(" email = " + email + " | " );       
           strUserProfile.Append(" language = " + language + " | " );       
            strUserProfile.Append("] ");    
    
    		return strUserProfile.ToString();
        }
    
    
    }
}
