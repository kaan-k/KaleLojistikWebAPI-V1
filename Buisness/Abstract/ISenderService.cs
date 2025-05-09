﻿using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface ISenderService
    {
        IResult Add(Sender sender);
        IResult Update(Sender sender, string id);
        IResult Delete(string id);

        IDataResult<Sender> GetById(string id);
        IDataResult<List<Sender>> GetAll();

    }
}
