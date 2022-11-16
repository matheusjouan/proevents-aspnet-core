//using FluentValidation.Results;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ProEvents.Infrastructure.Deletar;
//public abstract class ValidationResultErrors
//{
//    protected ValidationResult ValidationResult;

//    protected ValidationResultErrors()
//    {
//        ValidationResult = new ValidationResult();
//    }

//    protected void AddError(string message)
//    {
//        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
//    }
//}


///*
//    Os services herdaram dessa classe
//    Todos os retornos do Service serão do tipo ValidationResult
//    A cada erro, utiliza o método AddError

//    Na Controller 
//    var result = await _nomeService()

//    if (!result.IsValid)
//        AddErrors()
 
// */