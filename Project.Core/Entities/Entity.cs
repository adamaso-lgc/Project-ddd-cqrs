﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Entities
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
