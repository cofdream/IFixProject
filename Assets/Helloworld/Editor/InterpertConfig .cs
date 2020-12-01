using System.Collections.Generic;
using IFix;
using System;
using System.Reflection;
using System.Linq;

//1、配置类必须打[Configure]标签
//2、必须放Editor目录
[Configure]
public class InterpertConfig
{
    // 可能需要修复函数的类的集合
    [IFix]
    static IEnumerable<Type> ToProcess
    {
        get
        {
            return (from type in Assembly.Load("Assembly-CSharp").GetTypes()
                    select type).ToArray();
        }
    }
    // 过滤不想发生注入的函数	
    [IFix.Filter]
    static bool Filter(System.Reflection.MethodInfo methodInfo)
    {
        return methodInfo.DeclaringType.FullName == "IFix.Test.Calculator" && (methodInfo.Name == "Div" || methodInfo.Name == "Mult");
    }
}
