/*flexberryautogenerated="false"*/

namespace IIS.Практикум
{
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Web.Controls;
    using ICSSoft.STORMNET.Web.AjaxControls;
    using ICSSoft.STORMNET.FunctionalLanguage;
    using ICSSoft.STORMNET.FunctionalLanguage.SQLWhere;
    using System;
    using System.Collections.Generic;
    using System.Web.Services;
    using ICSSoft.STORMNET.Web.Tools;

    public partial class ЗаказE : BaseEditForm<Заказ>
    {
        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public ЗаказE()
            : base(Заказ.Views.ЗаказE)
        {
        }

        /// <summary>
        /// Путь до формы.
        /// </summary>
        public static string FormPath
        {
            get { return "~/forms/Orders/ZakazE.aspx"; }
        }

        /// <summary>
        /// Вызывается самым первым в Page_Load.
        /// </summary>
        protected override void Preload()
        {
            ctrlЦена.ReadOnly = true;
        }

        /// <summary>
        /// Здесь лучше всего писать бизнес-логику, оперируя только объектом данных.
        /// </summary>
        protected override void PreApplyToControls()
        {
            Function lf = FunctionBuilder.BuildEquals<Сотрудник>(x => x.Должность, Должность.Менеджер);

            ctrlМенеджер.LimitFunction = lf;
        }

        /// <summary>
        /// Здесь лучше всего изменять свойства контролов на странице,
        /// которые не обрабатываются WebBinder.
        /// </summary>
        protected override void PostApplyToControls()
        {
            if ((DataObject != null) && (DataObject.Статус == СостояниеЗаказа.Оплаченный))
            {
                wb.SetReadonlyToControl(ctrlСтатус, true);
            }

            Page.Validate();
        }

        /// <summary>
        /// Вызывается самым последним в Page_Load.
        /// </summary>
        protected override void Postload()
        {
        }

        /// <summary>
        /// Валидация объекта для сохранения.
        /// </summary>
        /// <returns>true - продолжать сохранение, иначе - прекратить.</returns>
        protected override bool PreSaveObject()
        {
            return base.PreSaveObject();
        }

        /// <summary>
        /// Нетривиальная логика сохранения объекта.
        /// </summary>
        /// <returns>Объект данных, который сохранился.</returns>
        protected override DataObject SaveObject()
        {
            return base.SaveObject();
        }

        /// <summary>
        /// Метод изменяющий LCS в лукапах, находящихся в AGE.
        /// </summary>
        /// <param name="ordKeys">Ключи.</param>
        /// <param name="lfKey">Ключ сессии.</param>
        /// <returns>Ключ сессии.</returns>
        [WebMethod]
        public static string CreateLf(string[] ordKeys, string lfKey)
        {
            if (string.IsNullOrEmpty(lfKey))
            {
                lfKey = Guid.NewGuid().ToString("B");
            }

            Function lf = FunctionBuilder.BuildNotIn(ordKeys);
            LimitFunctionsHolder.PersistLimitFunction(lfKey, lf);

            return lfKey;
        }
    }
}