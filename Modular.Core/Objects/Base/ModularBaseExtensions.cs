using Foundation;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core
{

    public struct Expression
    {
        public string PropertyName { get; set; }
        public string Value { get; set; }
        public ExpressionType Type { get; set; }

    }

    public static class ModularBaseExtension
    {

        public static List<ModularBase> Where(Expression<Func<ModularBase, bool>> Predicate)
        {
            // if (Predicate != null)
            // {
            //     List<Expression> AllExpressions = new List<Expression>();
            // 
            //     System.Linq.Expressions.Expression Body = Predicate.Body;
            // 
            //     if (Body is BinaryExpression BinaryExpression)
            //     {
            // 
            //         if (BinaryExpression.NodeType.Equals(ExpressionType.And) || BinaryExpression.NodeType.Equals(ExpressionType.AndAlso) || BinaryExpression.NodeType.Equals(ExpressionType.Or) || BinaryExpression.NodeType.Equals(ExpressionType.OrElse))
            //         { 
            //             AllExpressions.AddRange(GetConditions(BinaryExpression))
            //         }
            // 
            // 
            // 
            //         if (BinaryExpression.Left is MemberExpression MemberExpression)
            //         {
            //             string PropertyName = MemberExpression.Member.Name ;
            //         }
            // 
            //         if (BinaryExpression.Right is ConstantExpression ConstantExpression)
            //         {
            //             object PropertyValue = ConstantExpression.Value;
            //             PropertyValue.ToString();
            //         }
            // 
            // 
            // 
            // 
            //     }
            // 
            // 
            // 
            // }
            // else
            // {
            return ModularBase.Instances.All();
            //}

        }
    }
}
