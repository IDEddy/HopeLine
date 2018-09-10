﻿namespace HopeLine.DataAccess.Entities
{
    /// <summary>
    /// This class is for mentors who are hired by Admins
    /// </summary>
    public class MentorAccount : HopeLineUser
    {
        /// <summary>
        /// Initialize all default properties
        /// </summary>
        public MentorAccount()
        {
            AccountType = Account.Mentor;
        }

        //TODO : add more properties like availabilitie and Schedule
        public Profile Profile { get; set; }
    }
}
