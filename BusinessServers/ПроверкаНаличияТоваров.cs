﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IIS.Практикум
{
    using System;
    using System.Xml;
    using System.Collections;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.FunctionalLanguage;
    using ICSSoft.STORMNET.FunctionalLanguage.SQLWhere;


    // *** Start programmer edit section *** (Using statements)

    // *** End programmer edit section *** (Using statements)


    /// <summary>
    /// ПроверкаНаличияТоваров.
    /// </summary>
    // *** Start programmer edit section *** (ПроверкаНаличияТоваров CustomAttributes)

    // *** End programmer edit section *** (ПроверкаНаличияТоваров CustomAttributes)
    [ICSSoft.STORMNET.AccessType(ICSSoft.STORMNET.AccessType.none)]
    public class ПроверкаНаличияТоваров : ICSSoft.STORMNET.Business.BusinessServer
    {

        // *** Start programmer edit section *** (ПроверкаНаличияТоваров CustomMembers)

        // *** End programmer edit section *** (ПроверкаНаличияТоваров CustomMembers)


        // *** Start programmer edit section *** (OnUpdateЗаказ CustomAttributes)

        // *** End programmer edit section *** (OnUpdateЗаказ CustomAttributes)
        public virtual ICSSoft.STORMNET.DataObject[] OnUpdateЗаказ(Практикум.Заказ UpdatedObject)
        {
            // *** Start programmer edit section *** (OnUpdateЗаказ)

            // Определим массив, который будем возвращать для обновления.
            DataObject[] ret = new DataObject[0];

            // Проверим  на  то,  что  пришедший  объект  -  это	именно то, что нам нужно (создан или изменён и статус установлен в Оплачено).		
            if ((UpdatedObject.GetStatus() == ICSSoft.STORMNET.ObjectStatus.Created || UpdatedObject.GetStatus() == ICSSoft.STORMNET.ObjectStatus.Altered) && Array.IndexOf(UpdatedObject.GetAlteredPropertyNames(), "Статус") >= 0 && UpdatedObject.Статус == СостояниеЗаказа.Оплаченный)
            {
                // Построим ограничение и вычитаем все объекты ТоварНаСкладе, которые нам подходят.
                Заказ заказ = UpdatedObject;
                ICSSoft.STORMNET.FunctionalLanguage.Function lf = null;

                for (int i = 0; i < заказ.СтрокаЗаказа.Count; i++)
                {
                    if (lf != null)
                    {
                        if (заказ.СтрокаЗаказа[i].Товар != null)
                            lf = FunctionBuilder.BuildOr(
                                    lf,
                                    FunctionBuilder.BuildEquals<ТоварНаСкладе>(x => x.Товар, заказ.СтрокаЗаказа[i].Товар));
                    }

                    else
                    {
                        if (заказ.СтрокаЗаказа[i].Товар != null)
                            lf = FunctionBuilder.BuildEquals<ТоварНаСкладе>(x => x.Товар, заказ.СтрокаЗаказа[i].Товар);
                    }
                }

                ICSSoft.STORMNET.Business.LoadingCustomizationStruct lcs = ICSSoft.STORMNET.Business.LoadingCustomizationStruct.GetSimpleStruct(typeof(ТоварНаСкладе), "ТоварНаСкладеE");
                lcs.LimitFunction = lf;
                ICSSoft.STORMNET.DataObject[] objs = ICSSoft.STORMNET.Business.DataServiceProvider.DataService.LoadObjects(lcs);

                // Разместим вычитанные объекты в отсортированном списке для удобного доступа в дальнейшем.
                System.Collections.SortedList sl = new System.Collections.SortedList();

                for (int i = 0; i < objs.Length; i++)
                {
                    if (sl.ContainsKey(((ТоварНаСкладе)objs[i]).Товар.__PrimaryKey))
                    {
                        ((System.Collections.ArrayList)sl[objs[i].__PrimaryKey]).Add(objs[i]);
                    }
                    else
                    {
                        System.Collections.ArrayList списокТоваров = new System.Collections.ArrayList();
                        списокТоваров.Add(objs[i]);
                        sl.Add(((ТоварНаСкладе)objs[i]).Товар.__PrimaryKey, списокТоваров);
                    }
                }

                // Определим строчку для сообщения об ошибке. 
                string errStr = string.Empty;
                ArrayList retObjs = new ArrayList();

                // Проверим наличие товара на складах, если не хватает, то выдадим сообщение об ошибке, если хватает, то вычитаем количество.
                for (int i = 0; i < заказ.СтрокаЗаказа.Count; i++)
                {
                    if (sl.ContainsKey(заказ.СтрокаЗаказа[i].Товар.__PrimaryKey))
                    {
                        ArrayList arl = ((System.Collections.ArrayList)sl[заказ.СтрокаЗаказа[i].Товар.__PrimaryKey]);

                        int количествоНаСкладах = 0; for (int j = 0; j < arl.Count; j++)
                        {
                            количествоНаСкладах +=
                            ((ТоварНаСкладе)arl[j]).Количество;
                        }

                        if (количествоНаСкладах <
                        заказ.СтрокаЗаказа[i].Количество)
                        {
                            errStr += " Не хватает товара \"" + заказ.СтрокаЗаказа[i].Товар.Название + "\" в наличии: " + количествоНаСкладах + ", требуется " + заказ.СтрокаЗаказа[i].Количество + Environment.NewLine;
                        }
                        else
                        {
                            int колич = заказ.СтрокаЗаказа[i].Количество;
                            for (int j = 0; j < arl.Count; j++)
                            {
                                if (колич > 0 &&
                                ((ТоварНаСкладе)arl[j]).Количество > колич)
                                {
                                    ((ТоварНаСкладе)arl[j]).Количество -= колич;
                                    колич = 0; retObjs.Add(arl[j]);
                                }
                                else
                                {
                                    if (колич > 0)
                                    {
                                        колич -= ((ТоварНаСкладе)arl[j]).Количество;
                                        ((ТоварНаСкладе)arl[j]).Количество = 0;
                                        retObjs.Add(arl[j]);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        errStr += "Товар	\"" +
                        заказ.СтрокаЗаказа[i].Товар.Название + "\" в наличии отсутствует." + Environment.NewLine;
                    }
                }

                // В случае, если чего-то не хватило, сообщаем об этом пользователю.
                if (errStr != string.Empty)
                {
                    throw new Exception(errStr);
                }

                // Если всё нормально, то возвращаем массив объектов, которые надо обновить.
                ret = new DataObject[retObjs.Count]; retObjs.CopyTo(ret, 0);
            }
            return ret;

            // *** End programmer edit section *** (OnUpdateЗаказ)
        }
    }
}
