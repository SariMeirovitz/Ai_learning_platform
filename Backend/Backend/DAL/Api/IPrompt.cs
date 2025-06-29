﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public interface IPrompt: ICrud<Prompt>
    {
        public IEnumerable<Prompt> GetByUserId(int userId);
    }
}
