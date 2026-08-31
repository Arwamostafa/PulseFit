namespace PulseFit.PL.Extensions;

public static class ControllerExtentions
{
    public static IActionResult ViewIndex<T>(this Controller controller, Results<T> results, string error)
    {
        if (!results.IsSuccess)
        {
            controller.TempData["ErrorMessage"] = results.Error ?? error;
            controller.View(Array.Empty<T>());

        }
        return controller.View(results.Data);
    }

    public static IActionResult ViewFormForModelStateOnFaliure<T>(this Controller controller, result result, string tempError, T model)
    {

        controller.ModelState.AddModelError(result.Property!, result.Error!);
        controller.TempData["ErrorMessage"] = tempError;
        return controller.View(model);
    }

    public static IActionResult ViewDetails<T>(this Controller controller, Results<T> results, string tempError, string ActionName)
    {
        if (!results.IsSuccess)
        {
            controller.TempData["ErrorMessage"] = tempError;
            controller.RedirectToAction(ActionName);
        }
        return controller.View(results.Data);

    }

    public static IActionResult Update<T>(this Controller controller, result results, T model, string ActionName)
    {
        if (!results.IsSuccess)
        {
            controller.ModelState.AddModelError(string.Empty, results.Error);
            controller.TempData["ErrorMessage"] = "Error happened while updating member!";
            return controller.View("GetMemberToUpdate", model);
        }

        controller.TempData["SuccessMessage"] = "Member updated successfully.";
        return controller.RedirectToAction(ActionName);
    }
}

