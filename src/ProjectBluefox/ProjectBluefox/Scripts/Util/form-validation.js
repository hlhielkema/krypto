if (window.krypto == undefined) {
    window.krypto = {};
}

// Krypto form validation utilities
window.krypto.validation = {

    // Set the validation error message for a form element
    setMessage: function (formSelector, name, message) {
        // Use the default form selector when its not defined
        if (formSelector === undefined || formSelector === null) {
            formSelector = 'form';
        }

        // Get the form element
        var $form = $(formSelector);
        if ($form.length === 0) {
            throw 'Form not found for selector: ' + formSelector;
        }        

        // Get the form element
        var $element = $form.find('.form-element[data-name="' + name + '"]');

        // Create a new form element for validation that are not connected to one form element.
        // For example: invalid credentials can be caused by a wrong username or wrong password.
        if (name === '*' && $element.length === 0) {
            $element = $('<div>', {
                'class': 'form-element error-only',
                'data-name': '*'
            });
            $element.insertAfter($form.find('.form-element').last());            
        }

        // Continue if the form element exists
        if ($element.length > 0) {

            // Add the "has-validation-errors" class to the form element div
            if (!$element.hasClass('has-validation-errors')) {
                $element.addClass('has-validation-errors');
            }

            // Try to find the validation error message element
            var $message = $element.find('.validation-error-message');

            // Update OR create the validation message element
            if ($message.length > 0) {
                // Update the text of the validation message element
                $message.text(message);
            }
            else {
                // Add the validation error message element
                $element.append($('<div>', {
                    'class': "validation-error-message",
                    'text': message
                }));
            }
        }
    },

    // Set the validation error message for multiple messages
    setMessages: function (formSelector, messages) {
        // Use the default form selector when its not defined
        if (formSelector === undefined || formSelector === null) {
            formSelector = 'form';
        }

        for (var name in messages) {
            window.krypto.validation.setMessage(formSelector, name, messages[name]);
        }
    },

    // Clear a validation error message
    clearMessage: function (formSelector, name, message) {
        // Use the default form selector when its not defined
        if (formSelector === undefined || formSelector === null) {
            formSelector = 'form';
        }

        // Get the form element
        var $form = $(formSelector);
        if ($form.length === 0) {
            throw 'Form not found for selector: ' + formSelector;
        }

        // Get the form element
        var $element = $form.find('.form-element[data-name="' + name + '"]');

        // Continue if the form element exists
        if ($element.length > 0) {

            // Remove the "has-validation-errors" class from the form element div
            if ($element.hasClass('has-validation-errors')) {
                $element.removeClass('has-validation-errors');
            }

            // Remove the validation error message element
            $element.find('.validation-error-message').remove();
        }
    },

    // Clear all validation error messages
    clearAllMessages: function (formSelector) {
        // Use the default form selector when its not defined
        if (formSelector === undefined || formSelector === null) {
            formSelector = 'form';
        }

        // Get the form element
        var $form = $(formSelector);        
        if ($form.length === 0) {
            throw 'Form not found for selector: ' + formSelector;
        }

        // Get the form elements
        var $elements = $form.find('.form-element');

        // Loop through the form element
        for (var i = 0; i < $elements.length; i++) {
            // Get the current element to clear
            var $element = $($elements[i]);

            // Remove the "has-validation-errors" class from the form element div
            if ($element.hasClass('has-validation-errors')) {
                $element.removeClass('has-validation-errors');
            }

            // Remove the validation error message element
            var $message = $element.find('.validation-error-message').remove();            
        }
    },

    // Create an error handler for jQuery AJAX requests
    // Add this line inside the data of a "$.ajax({})":
    // error: krypto.validation.createAjaxErrorHandler('form'),
    createAjaxErrorHandler: function (formSelector, alternativeErrorHandler) {
        // Use the default form selector when its not defined
        if (formSelector === undefined || formSelector === null) {
            formSelector = 'form';
        }

        return function (result) {
            if (result.status === 412) { // 412 = Validation error
                krypto.validation.clearAllMessages(formSelector);
                krypto.validation.setMessages(formSelector, result.responseJSON);
            }
            else if (alternativeErrorHandler !== undefined) {
                alternativeErrorHandler(result);
            }
        }
    }
};