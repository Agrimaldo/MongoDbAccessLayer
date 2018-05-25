using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Rule.Util
{
    public static class CustomExtension
    {
        public static Expression<Func<T, bool>> TreatNullExpression<T>(this Expression<Func<T, bool>> _expression)
        {
            try
            {
                if (_expression == null)
                    _expression = p => true;
            }

            catch (Exception)
            {
                throw;
            }

            return _expression;
        }

        public static ObjectId GetId<T>(this T _obj)
        {
            try
            {
                PropertyInfo _property = _obj.GetType().GetProperties().ToList().Where(a => a.Name.LastIndexOf("Id", 0) < 3).First();
                return (ObjectId)_obj.GetType().GetProperty(_property.Name).GetValue(_obj, null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int GetEnumIndex<T>(this string _description)
        {
            int _index = -1;

            MemberInfo[] _membersInfo = typeof(T).GetMembers();

            if (_membersInfo != null && _membersInfo.Count() > 0)
            {
                _membersInfo.ToList().ForEach(member =>
                {
                    _index++;
                    object[] _attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);

                    if (_attributes != null && _attributes.Length > 0)
                    {
                        if (((DisplayAttribute)_attributes[0]).Name == _description)
                            return;
                    }
                });
            }

            return _index;
        }

        public static string GetDescription(this Enum _enumerator)
        {
            Type _type = _enumerator.GetType();

            MemberInfo[] _memberInfo = _type.GetMember(_enumerator.ToString());

            if (_memberInfo != null && _memberInfo.Length > 0)
            {
                object[] _attributes = _memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);

                if (_attributes != null && _attributes.Length > 0)
                    return ((DisplayAttribute)_attributes[0]).Name;

            }
            return _enumerator.ToString();
        }
    }
}
