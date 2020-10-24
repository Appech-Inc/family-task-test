﻿using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModel
{
    public class TaskVm
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public Nullable<Guid> AssignedMemberId { get; set; }
        public bool IsComplete { get; set; }


    }
}
