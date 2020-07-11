using System;

namespace Relays.Extensions
{
    public static class ObjectExtensions
    {
        public static Tout Transform<Tin, Tout>(this Tin @this, Func<Tin, Tout> transform) 
            => transform(@this);
    }
}