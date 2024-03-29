﻿using System;

namespace Api.Domain.Models
{
    public class UserModel
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private DateTime _dateCreate;
        public DateTime DateCreate
        {
            get { return _dateCreate; }
            set
            {
                _dateCreate = value;
            }
        }

        private DateTime? _dateUpdate;
        public DateTime? DateUpdate
        {
            get { return _dateUpdate; }
            set { _dateUpdate = value; }
        }

    }
}
