using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace TrucoAPI.DataAnottations
{
    public class MaxPlayersCount : ValidationAttribute
    {
        private readonly int _max;

        public MaxPlayersCount(int max)
        {
            _max = max;
            ErrorMessage = $"A lista não pode conter mais que {_max} itens.";
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;

            if (value is ICollection collection)
            {
                return collection.Count <= _max;
            }

            return false;
        }

    }

}
