﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace EvernoteClone.Model
{
    public class Note : HasId
    {
        public string Id { get; set; }
        public string NotebookId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FileLocation { get; set; }
    }
}
