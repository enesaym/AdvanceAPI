using AdvanceApi.BLL.Mapper;
using AdvanceApi.CORE.Entities;
using AdvanceApi.CORE.Response;
using AdvanceApi.DAL.UnitOfWork;
using AdvanceApi.DTO.Advance;
using AdvanceApi.DTO.AdvanceHistory;
using AdvanceApi.DTO.Employee;
using AdvanceApi.DTO.Project;
using AdvanceApi.DTO.Status;
using AdvanceApi.LOG.Log;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.BLL.Manager
{
	public class AdvanceManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly MyMapper _mapper;
		private readonly ILog _logger;
		public AdvanceManager(IUnitOfWork unitOfWork,MyMapper mapper,ILog logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
			_logger = logger;
        }

        /// <summary>
        /// for insert Advance and advanceHistory table
        /// </summary>
        /// <param name="advance"></param>
        /// <returns>Inserted Advance ID</returns>
        public async Task<ApiResponse<AdvanceInsertDTO>> InsertAdvanceAndHistory(AdvanceInsertDTO advance)
        {
			_unitOfWork.BeginTransaction();
			try
			{
				var mapped= _mapper.Map<AdvanceInsertDTO, Advance>(advance);
				mapped.StatusID = 101;
				mapped.RequestDate = DateTime.Now;
				int insertedAdvanceId = await _unitOfWork.AdvanceDAL.AdvanceInsert(mapped);

				if (insertedAdvanceId > 0)
				{
					// İkinci tabloya insert
					AdvanceHistoryInsertDTO advanceHistory = new AdvanceHistoryInsertDTO();
					advanceHistory.AdvanceID = insertedAdvanceId;
					advanceHistory.TransactorID = advance.EmployeeID;
					//talep ilk olusturuldugunda 201 = talep olusturuldu
					advanceHistory.StatusID = 201;
					advanceHistory.Date=DateTime.Now;
					advanceHistory.IsActive = true;
					advanceHistory.ApprovedAmount = advance.AdvanceAmount;
					var advanceHistoryMapped=_mapper.Map<AdvanceHistoryInsertDTO, AdvanceHistory>(advanceHistory);

					var insertedAdvanceHistory=await _unitOfWork.AdvanceHistoryDAL.InsertAdvanceHistory(advanceHistoryMapped);

					_unitOfWork.Commit();

                    //eklenen advance id döner
                    //buradaki loglar enumdan alınabilir
                    _logger.TakeInfoLog(advance.EmployeeID + "ID li kullanıcı avans talebi olusturdu");
                    return new ApiResponse<AdvanceInsertDTO>(advance);
                  
                }
				else
				{
					
					return new ApiResponse<AdvanceInsertDTO>("Hata olustu"); 
				}
			}
			catch (Exception ex)
			{
				_unitOfWork.RollBack();
                _logger.TakeErrorLog(advance.EmployeeID + "ID li kullanıcı avans talebi olusturulurken hata");
                return new ApiResponse<AdvanceInsertDTO>(ex.Message);
			}
			finally 
			{
			_unitOfWork.TransactionDispose();

			}
		}

		
		public async Task<ApiResponse<List<AdvanceSelectDTO>>> GetAdvanceAndHistory(int employeeID)
		{
			try
			{
                var result = await _unitOfWork.AdvanceDAL.GetEmployeeAdvances(employeeID);
                foreach (var item in result)
                {
					//genel mudur onayladi ise avans statusu onaylandi olarak belirlenir
					if (item.AdvanceHistories.LastOrDefault().StatusID.Value == 205)
					{
                        var status = await _unitOfWork.StatusDAL.GetStatusById
                       (102);
                        item.Status = status;
                    }
					else
					{
                        var status = await _unitOfWork.StatusDAL.GetStatusById
						(item.AdvanceHistories.LastOrDefault().StatusID.Value);
                        item.Status = status;
                    }
                   

                    var project = await _unitOfWork.ProjectDAL.GetProjectById(item.ProjectID.Value);
                    item.Project = project;
                }
          
                var mapped=_mapper.Map<List<Advance>,List<AdvanceSelectDTO>> (result);

				return new ApiResponse<List<AdvanceSelectDTO>>(mapped);

            }
			catch (Exception ex)
			{
                return new ApiResponse<List<AdvanceSelectDTO>>(ex.Message);
           
			}
		
        }

		/// <summary>
		/// take employee ıd and return approval advances 
		/// </summary>
		/// <param name="employeeID"></param>
		/// <returns>Aprooval advance histories</returns>
        public async Task<ApiResponse<List<AdvanceHistorySelectDTO>>> GetPendingApprovalAdvances(int employeeID)
        {
            try
            {
				//finans mudurune status 102 olan gosterilir
				var emp = await _unitOfWork.EmployeeDAL.GetByEmployeeId(employeeID);
				var result = new List<AdvanceHistory>();
				if (emp.TitleID==3)
				{
                    result = await _unitOfWork.AdvanceHistoryDAL.GetPendingApprovalAdvancesFM(employeeID);
                }
				else if (emp.TitleID == 6)
				{
                    result = await _unitOfWork.AdvanceHistoryDAL.GetPendingApprovalAdvancesAccountant(employeeID);
                }
				else
				{
                    result = await _unitOfWork.AdvanceHistoryDAL.GetPendingApprovalAdvances(employeeID);
                }
                
               
                foreach (var item in result)
                {
					//status ıd sıne gore status doldurulur
					item.Status = await _unitOfWork.StatusDAL.GetStatusById(item.StatusID.Value);
					//title id ye gore transactor title doldurulur
					item.Transactor.Title=await _unitOfWork.TitleDAL.GetByTitleId(item.Transactor.TitleID.Value);
					//avans isteyenin project nesnesinin alınması
                    var project = await _unitOfWork.ProjectDAL.GetProjectById(item.Advance.ProjectID.Value);
                    item.Advance.Project = project;
				
				}

                var mapped = _mapper.Map<List<AdvanceHistory>, List<AdvanceHistorySelectDTO>>(result);

                return new ApiResponse<List<AdvanceHistorySelectDTO>>(mapped);

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<AdvanceHistorySelectDTO>>(ex.Message);
            }

        }
       

        public async Task<ApiResponse<List<AdvanceHistorySelectDTO>>> GetAdvanceHistoryByAdvanceId(int advanceID)
        {
            try
            {
                var result = await _unitOfWork.AdvanceHistoryDAL.GetAdvanceHistoryByAdvanceId(advanceID);
                var mapped = _mapper.Map<List<AdvanceHistory>, List<AdvanceHistorySelectDTO>>(result);
				//sonraki status ve kullanici verme
                foreach (var item in mapped)
                {
					//sonraki status id getir
				
                    if (item.Status.ID!=207 && item.Status.ID!=103 && item.Status.ID != 102)
					{
						var afterStatus = new StatusSelectDTO();
						afterStatus.ID = (item.Status.ID) + 1;
						var afterStatusName = await _unitOfWork.StatusDAL.GetStatusById((item.Status.ID) + 1);
						afterStatus.StatusName = afterStatusName.StatusName;
						item.AfterStatus = afterStatus;

					}
					else
					{
                        var afterStatus = new StatusSelectDTO();
						item.AfterStatus = afterStatus;
                        item.AfterStatus = item.Status;
					}
					//sonraki kullanıcı
					//var employee = new EmployeeSelectDTO();
                    var afterEmployee = await _unitOfWork.EmployeeDAL.GetByEmployeeId(item.Transactor.UpperEmployeeID.Value);
					item.AfterEmployee = new EmployeeSelectDTO();
					item.AfterEmployee.ID = afterEmployee.ID;
					item.AfterEmployee.Name = afterEmployee.Name;
					item.AfterEmployee.TitleID = afterEmployee.TitleID.Value;  
                }
                return new ApiResponse<List<AdvanceHistorySelectDTO>>(mapped);

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<AdvanceHistorySelectDTO>>(ex.Message);
            }

        }

		public async Task<ApiResponse<AdvanceRejectDTO>> RejectAdvance(AdvanceRejectDTO reject)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				//advance history eklenir ->> reddedildi
				AdvanceHistory advanceHistory = new AdvanceHistory();
				advanceHistory.TransactorID = reject.EmployeeID;
				advanceHistory.StatusID = 103;
				advanceHistory.AdvanceID= reject.AdvanceID;
				advanceHistory.Date = DateTime.Now;
				advanceHistory.ApprovedAmount = 0;
				var insertedAdvanceHistory = await _unitOfWork.AdvanceHistoryDAL.InsertAdvanceHistory(advanceHistory);

				if (insertedAdvanceHistory != null)
				{
					var result= await _unitOfWork.AdvanceDAL.UpdateAdvanceStatus(reject.AdvanceID);
					if (result==true)
					{
						_unitOfWork.Commit();
						return new ApiResponse<AdvanceRejectDTO>(reject);
					}
					else
					{
					
						return new ApiResponse<AdvanceRejectDTO>("Hata olustu");
					}
				}
				else
				{

					return new ApiResponse<AdvanceRejectDTO>("Hata olustu");
				}
			}
			catch (Exception ex)
			{
				_unitOfWork.RollBack();
				return new ApiResponse<AdvanceRejectDTO>(ex.Message);
			}
			finally
			{
				_unitOfWork.TransactionDispose();

			}
		}

		public async Task<ApiResponse<AdvanceApproveDTO>> ApproveAdvance(AdvanceApproveDTO approve)
		{
			try
			{
				var result = await IsItEnough(approve.ApprovedAmount, approve.TitleID);
				if (result)
				{
					var dto = await ApproveEnoughAdvance(approve);
                    return dto;
                }
				else
				{
                    //avans statusu bir sonrakine aktarılır ve history tablosuna ekleme yapılır
                    var dto = await ApproveIsNotEnoughAdvance(approve);
                    return dto;
                }
			}
			catch (Exception ex)
			{
		
				return new ApiResponse<AdvanceApproveDTO>(ex.Message);
			}
			finally
			{
			

			}
		}
		//title ın onaylayabilecegi tutar yeterli ise avans direkt onaylanır
		public async Task<bool> IsItEnough(decimal amount,int titleID)
		{
			var rule=await _unitOfWork.RuleDAL.GetRuleByTitleId(titleID);

            return rule.MaxAmount >= amount;
        }

        //fm tarih belirleme
        public async Task<ApiResponse<FMApproveAdvanceDTO>> ApproveAdvanceFM(FMApproveAdvanceDTO approve)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                //avans history onaylandı kaydı eklenir ve avans statusu onaylandı yapılır
                AdvanceHistory advanceHistory = new AdvanceHistory();
                advanceHistory.TransactorID = approve.EmployeeID;
                advanceHistory.StatusID = 206;
                advanceHistory.AdvanceID = approve.AdvanceID;
                //onceki butun historyleri disable eder

                await _unitOfWork.AdvanceHistoryDAL.UpdateAdvanceHistoriesByAdvanceID(approve.AdvanceID);

                advanceHistory.IsActive = true;
                advanceHistory.Date = DateTime.Now;
                advanceHistory.ApprovedAmount = approve.ApprovedAmount;
                var insertedAdvanceHistory = await _unitOfWork.AdvanceHistoryDAL.InsertAdvanceHistory(advanceHistory);

                if (insertedAdvanceHistory != null)
                {
                    Payment payment = new Payment();
                    payment.FinanceManagerID = approve.EmployeeID;
                    payment.AdvanceId = approve.AdvanceID;
                    payment.DeterminedPaymentDate = approve.DeterminedPaymentDate;
                    var result = await _unitOfWork.PaymentDAL.PaymentInsert(payment);
                    if (result > 0)
                    {
                        _unitOfWork.Commit();
                        return new ApiResponse<FMApproveAdvanceDTO>(approve);
                    }
                    else
                    {

                        return new ApiResponse<FMApproveAdvanceDTO>("Hata olustu");
                    }
                }
                else
                {

                    return new ApiResponse<FMApproveAdvanceDTO>("Hata olustu");
                }

            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return new ApiResponse<FMApproveAdvanceDTO>(ex.Message);
            }
            finally
            {
                _unitOfWork.TransactionDispose();

            }

        }

        //muhasebeci onaylama
        public async Task<ApiResponse<AccountantApproveDTO>> ApproveAdvanceAccountant(AccountantApproveDTO approve)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                //avans history onaylandı kaydı eklenir ve avans statusu onaylandı yapılır
                AdvanceHistory advanceHistory = new AdvanceHistory();
                advanceHistory.TransactorID = approve.AccountantID;
                advanceHistory.StatusID = 207;
                advanceHistory.ApprovedAmount = approve.ApprovedAmount;
                advanceHistory.AdvanceID = approve.AdvanceID;
                //onceki butun historyleri disable eder

                await _unitOfWork.AdvanceHistoryDAL.UpdateAdvanceHistoriesByAdvanceID(approve.AdvanceID);

                advanceHistory.IsActive = true;
                advanceHistory.Date = DateTime.Now;
                var insertedAdvanceHistory = await _unitOfWork.AdvanceHistoryDAL.InsertAdvanceHistory(advanceHistory);

                if (insertedAdvanceHistory != null)
                {
                    Receipt receipt = new Receipt();
                    receipt.AccountantID = approve.AccountantID;
                    receipt.AdvanceID = approve.AdvanceID;
                    receipt.isRefundReceipt = false;
                    receipt.ReceiptNo=approve.ReceiptNo;
                    receipt.Date = DateTime.Now;
                    var result = await _unitOfWork.ReceiptDAL.ReceiptInsert(receipt);
                    if (result > 0)
                    {
                        _unitOfWork.Commit();
                        return new ApiResponse<AccountantApproveDTO>(approve);
                    }
                    else
                    {

                        return new ApiResponse<AccountantApproveDTO>("Hata olustu");
                    }
                }
                else
                {

                    return new ApiResponse<AccountantApproveDTO>("Hata olustu");
                }

            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return new ApiResponse<AccountantApproveDTO>(ex.Message);
            }
            finally
            {
                _unitOfWork.TransactionDispose();

            }

        }
        //yeterli yetki icin onaylama senaryosu
        public async Task<ApiResponse<AdvanceApproveDTO>> ApproveEnoughAdvance(AdvanceApproveDTO approve)
		{
            try
            {
                _unitOfWork.BeginTransaction();
                //avans history onaylandı kaydı eklenir ve avans statusu onaylandı yapılır
                AdvanceHistory advanceHistory = new AdvanceHistory();
                advanceHistory.TransactorID = approve.EmployeeID;
                advanceHistory.StatusID = 102;
                advanceHistory.AdvanceID = approve.AdvanceID;

				//onceki butun historyleri disable eder

				await _unitOfWork.AdvanceHistoryDAL.UpdateAdvanceHistoriesByAdvanceID(approve.AdvanceID);

				advanceHistory.IsActive = true;
                advanceHistory.Date = DateTime.Now;
                advanceHistory.ApprovedAmount = approve.ApprovedAmount;
                var insertedAdvanceHistory = await _unitOfWork.AdvanceHistoryDAL.InsertAdvanceHistory(advanceHistory);

                if (insertedAdvanceHistory != null)
                {
                    var result = await _unitOfWork.AdvanceDAL.UpdateAdvanceStatus(approve.AdvanceID);
                    if (result == true)
                    {
                        _unitOfWork.Commit();
                        return new ApiResponse<AdvanceApproveDTO>(approve);
                    }
                    else
                    {

                        return new ApiResponse<AdvanceApproveDTO>("Hata olustu");
                    }
                }
                else
                {

                    return new ApiResponse<AdvanceApproveDTO>("Hata olustu");
                }

            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return new ApiResponse<AdvanceApproveDTO>(ex.Message);
            }
            finally
            {
                _unitOfWork.TransactionDispose();

            }
           
        }

		//yetki yeterli degilse onaylama senaryosu
        public async Task<ApiResponse<AdvanceApproveDTO>> ApproveIsNotEnoughAdvance(AdvanceApproveDTO approve)
        {
            try
            {
                //avans history onaylandı kaydı eklenir ve avans statusu onaylandı yapılır
                AdvanceHistory advanceHistory = new AdvanceHistory();
                advanceHistory.TransactorID = approve.EmployeeID;
                advanceHistory.StatusID = approve.StatusID+1;
                advanceHistory.AdvanceID = approve.AdvanceID;
				advanceHistory.IsActive = true;
                await _unitOfWork.AdvanceHistoryDAL.UpdateAdvanceHistoriesByAdvanceID(approve.AdvanceID);
                advanceHistory.Date = DateTime.Now;
                advanceHistory.ApprovedAmount = approve.ApprovedAmount;
                var insertedAdvanceHistory = await _unitOfWork.AdvanceHistoryDAL.InsertAdvanceHistory(advanceHistory);

                return new ApiResponse<AdvanceApproveDTO>("Hata olustu");
                

            }
            catch (Exception ex)
            {
           
                return new ApiResponse<AdvanceApproveDTO>(ex.Message);
            }
         

        }
		


    }
}
