using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public class WeightRandom
    {
        /// <summary>
        /// 带权重的随机
        /// </summary>
        /// <param name="list">原始列表</param>
        /// <returns></returns>
        public static object Random(List<WeightObj> list, Random random = null)
        {
            if (list == null)
            {
                return null;
            }
            if (random == null)
            {
                random = new Random(Guid.NewGuid().GetHashCode());
            }
            list = list.Where(x => x.Weight >= 0).OrderBy(x => Guid.NewGuid()).ToList(); //对随机列表进行排序
            //计算权重总和
            int totalWeights = 0;
            for (int i = 0; i < list.Count; i++)
            {
                totalWeights += list[i].Weight;
            }
            int start = 0;
            //随机赋值权重
            List<WeightList> CalcList = new List<WeightList>();
            for (int i = 0; i < list.Count; i++)
            {
                CalcList.Add(new WeightList { Item = list[i].Item, Min = start + 1, Max = start + list[i].Weight });
                start = start + list[i].Weight;
            }
            var value = random.Next(1, totalWeights + 1);
            var obj = CalcList.Find(x => x.Min <= value && x.Max >= value);
            return obj.Item;
        }
        public static T Example<T>()
        {
            List<WeightObj> ranObj = new List<WeightObj> {
                new WeightObj () { Item = 1, Weight = 0 },
                new WeightObj () { Item = 2, Weight = 5 },
                new WeightObj () { Item = 3, Weight = 10 }
            };

            var CurItem = Random(ranObj);
            var result = (T)CurItem;

            return result;
        }
    }
}