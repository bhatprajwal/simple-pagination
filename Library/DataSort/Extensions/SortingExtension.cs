using Data.Sorts.Enums;
using Data.Sorts.Models;

namespace Data.Sorts.Extensions
{
    public static class SortingExtension
    {
        /// <summary>
        /// Returns the field order for sorting
        /// </summary>
        /// <param name="sortField">Requested Field to modify the order</param>
        /// <param name="field">Holds current state of Field & Order</param>
        /// <param name="defaultField"></param>
        /// <param name="isFieldOrderChanged"></param>
        /// <returns></returns>
        public static Field GetFieldSortOrder(this string sortField, Field field, string defaultField = "Id", bool isFieldOrderChanged = false)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                field = new Field()
                {
                    Name = defaultField
                    , Order = SortingOrder.Asc
                };
            }
            else
            {
                if(isFieldOrderChanged)
                {
                    if (field.Name == sortField)
                    {
                        field.Order = field.Order.ToString() == "Asc" ? SortingOrder.Desc : SortingOrder.Asc;
                    }
                    else
                    {
                        field.Order = SortingOrder.Asc;
                    }
                    field.Name = sortField;
                }
            }

            return field;
        }

        /// <summary>
        /// Returns sorted data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static List<T> GetSortedList<T>(this List<T> data, Field sort) where T : class
        {
            var propertyInfo = typeof(T).GetProperty(sort.Name);

            if (sort.Order == SortingOrder.Asc)
            {
                return data.OrderBy(s => propertyInfo.GetValue(s, null)).ToList();
            }
            else
            {
                return data.OrderByDescending(s => propertyInfo.GetValue(s, null)).ToList();
            }
        }
    }
}
