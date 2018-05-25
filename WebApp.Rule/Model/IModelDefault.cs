using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Rule.Model
{
    public interface IModelDefault
    {
        ObjectId Id { get; set; }

        bool Active { get; set; }

    }
}
