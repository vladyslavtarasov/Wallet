var GodObj = {};

function login(user_data) {
  $.post("https://localhost:7110/User/Login", user_data, function (data, status) {
    console.log("Data: " + data + "\nStatus: " + status);
    return status
  });
}

function onLoginInit() {
  $("#submit_login").click(function (e) {
    e.preventDefault();
    if ($("#user_name_login").val() != "" && $("#password_login").val() != "") {
      let new_user = {
        userName: $("#user_name_login").val(),
        password: $("#password_login").val(),
      };
      GodObj.userName = new_user.userName;//
      console.log(new_user);
      /*let status = login(new_user).then();
      console.log(status);*/
      let wrong = 1;
      $.post("https://localhost:7110/User/Login", new_user, function (data, status) {
        console.log("Data: " + data + "\nStatus: " + status);
        if (status == "success") {
          $('.container').addClass('hidden')
          $('.account-list').removeClass('hidden')
          $('.add').removeClass('hidden')
          $('.logout').removeClass('hidden')

          $.get(`https://localhost:7110/Account?userName=${new_user.userName}`, function(data){
            console.log(data);
            $(".account-list").html(data.map((account) => '<div class="account">\n' +
                `        <p class="account-name">${account.name}</p>\n` +
                `        <p class="account-balance">balance:<span class="account-balance-number">${account.balance}</span></p>\n` +
                `        <button id=${account.name} class="choose-button" onclick="onChooseAccountInit()">Choose</button>\n` +
                `        <button id=${account.userName} class="delete-button" onclick="onDeleteInit()">Delete</button>\n` +
                '    </div>'))
          });
          wrong = 0;
          console.log(wrong)
        }
      });
      setTimeout(function() {
        if (wrong == 1) {
          alert("Wrong password or username!");
        }
      }, 2000);
      //console.log(wrong)

      //location.replace('../wwwroot/accountsMenu.html')
    } else {
      alert("Fill all the fields!");
    }
  });
}

function onCreateAccountInit() {
  $("#submit_create_account").click(function (e) {
    e.preventDefault();
    if (/*$("#user_name_create_account").val() != "" && */$("#account_name_create_account").val() != ""
        && $("#balance_create_account").val() != ""){
      let new_account = {
        userName: GodObj.userName, //$("#user_name_create_account").val(),
        balance: $("#balance_create_account").val(),
        accountName: $("#account_name_create_account").val()
      };
      GodObj.userName = new_account.userName;
      console.log(new_account);

      $.post("https://localhost:7110/Account", new_account, function (data, status) {
        console.log("Data: " + data + "\nStatus: " + status);
        if (status == "success") {
          $('.modal').fadeOut();
          $.get(`https://localhost:7110/Account?userName=${new_account.userName}`, function(data){
            console.log(data);
            $(".account-list").html(data.map((account) => '<div class="account">\n' +
                `        <p id=${account.accountName} class="account-name">${account.name}</p>\n` +
                `        <p class="account-balance">balance:<span class="account-balance-number">${account.balance}</span></p>\n` +
                `        <button id=${account.name} class="choose-button" onclick="onChooseAccountInit()">Choose</button>\n` +
                `        <button id=${account.userName} class="delete-button" onclick="onDeleteInit()">Delete</button>\n` +
                '    </div>'))
          });
        }
      });
    } else {
      alert("Fill all the fields!");
    }
  });
}

function getAccounts() {
  $.get(`https://localhost:7110/Account?userName=${GodObj.userName}`, function(data){
    console.log(data);
    $(".account-list").html(data.map((account) => '<div class="account">\n' +
        `        <p class="account-name">${account.name}</p>\n` +
        `        <p class="account-balance">balance:<span class="account-balance-number">${account.balance}</span></p>\n` +
        `        <button class="choose-button">Choose</button>\n` +
        `        <button id=${account.userName} class="delete-button" onclick="onDeleteInit()">Delete</button>\n` +
        '    </div>'))
  });
}

function register(user_data) {
  $.post("https://localhost:7110/User/Register", user_data, function (data, status) {
    console.log("Data: " + data + "\nStatus: " + status);
  });
}

function onRegisterInit() {
  $("#submit").click(function (e) {
    e.preventDefault();
    if ($("#name").val() != "" && $("#surname").val() != "" && $("#user_name").val() != "" && $("#password").val() != "") {
      let new_user = {
        name: $("#name").val(),
        surname: $("#surname").val(),
        userName: $("#user_name").val(),
        password: $("#password").val(),
      };
      GodObj.userName = new_user.userName;
      console.log(new_user);
      let wrong = 1;
      //var result = register(new_user);
      $.post("https://localhost:7110/User/Register", new_user, function (data, status) {
        console.log("Data: " + data + "\nStatus: " + status);
        if (status == "success") {
          wrong = 0;
          $('.container').addClass('hidden')
          $('.account-list').removeClass('hidden')
          $('.add').removeClass('hidden')
          $('.logout').removeClass('hidden')

          $.get(`https://localhost:7110/Account?userName=${new_user.userName}`, function(data){
            console.log(data);
            $(".account-list").html(data.map((account) => '<div class="account">\n' +
                `        <p class="account-name">${account.name}</p>\n` +
                `        <p class="account-balance">balance:<span class="account-balance-number">${account.balance}</span></p>\n` +
                `        <button id=${account.name} class="choose-button" onclick="onChooseAccountInit()">Choose</button>\n` +
                `        <button id=${account.userName} class="delete-button" onclick="onDeleteInit()">Delete</button>\n` +
                '    </div>'))
          });
        }
      });
      setTimeout(function() {
        if (wrong == 1) {
          alert("This account already exists!");
        }
      }, 2000);
    } else {
      alert("Fill all the fields!");
    }
  });
}

function modalInit() {
  $('.modal').hide();
  $(".add").click(function () {
    $('.modal').fadeIn();
  });
  $(".close").click(function () {
    $('.modal').fadeOut();
  });
}

function onChooseAccountInit() {
  $(".choose-button").click(function () {
    console.log(123)
    let chooseAccountName = $(this).attr('id');
    let chooseUserName = GodObj.userName
    let chooseAccountBalance = $(this).parent().find('.account-balance').find('.account-balance-number').text();

    GodObj.accountName = chooseAccountName

    $('.accounts-wrapper').addClass('hidden')
    $('.profile-info').removeClass('hidden')
    $('.manage-buttons-wrapper').removeClass('hidden')

    $(".profile-info").html(
        `<div class="profile-user-name">User name:<span class="user-name-text">${chooseUserName}</span></div>\n` +
        `<div class="profile-account-name">Account name:<span class="account-name-text">${chooseAccountName}</span></div>\n` +
        `<div class="profile-balance">Balance:<span class="balance-number">${chooseAccountBalance}</span> </div>`)

    /* $(".manage-buttons-wrapper").html(`<button id="expense-statement-button">Create expense statement</button>
       <button id="income-statement-button">Create income statement</button>
       <button id="expense-category-button">Create expense category</button>
       <button id="expense-category-delete-button" class="red">Delete expense category</button>
       <button id="income-category-button" onclick="onCreateIncomeCategoryInit()">Create income category</button>
       <button id="income-category-delete-button" class="red">Delete income category</button>
       <button id="transfer-balance-button">Transfer balance</button>
       <button id="balance-history-button">Payment history</button>`)*/
  })
}

function onCreateIncomeCategoryInit() {
  /*$("#income-category-button").click(function (e) {
    //e.preventDefault();
    $('.modal-create-category').fadeIn();
  });*/
  $("#submit_create_income_category").click(function (e) {
    e.preventDefault();
    //$('.modal-create-category').fadeIn();*/
    if ($("#category-create-income-category").val() != ""){
      let incomeCategory = {
        userName: GodObj.userName,
        accountName: GodObj.accountName,
        categoryName: $("#category-create-income-category").val()
      }
      $.post("https://localhost:7110/IncomeCategory", incomeCategory, function( data, status ){
        console.log("Data: " + data + "\nStatus: " + status);
        if (status == "success") {
          $('.modal-create-income-category').fadeOut();
        }
      });
    } else {
      alert("Fill all the fields!");
    }
  });
}

function onCreateExpenseCategoryInit() {
  /*$("#income-category-button").click(function (e) {
    //e.preventDefault();
    $('.modal-create-category').fadeIn();
  });*/
  $("#submit_create_expense_category").click(function (e) {
    e.preventDefault();
    //$('.modal-create-category').fadeIn();*/
    if ($("#category-create-expense-category").val() != ""){
      let expenseCategory = {
        userName: GodObj.userName,
        accountName: GodObj.accountName,
        categoryName: $("#category-create-expense-category").val()
      }
      $.post("https://localhost:7110/ExpenseCategory", expenseCategory, function( data, status ){
        console.log("Data: " + data + "\nStatus: " + status);
        if (status == "success") {
          $('.modal-create-expense-category').fadeOut();
        }
      });
    } else {
      alert("Fill all the fields!");
    }
  });
}

function onDeleteIncomeCategoryInit() {
  $("#submit_delete_income_category").click(function (e) {
    e.preventDefault();
    //$('.modal-create-category').fadeIn();*/
    if ($("#delete-income-category-select").val() != ""){
      let incomeCategory = {
        userName: GodObj.userName,
        accountName: GodObj.accountName,
        categoryName: $("#delete-income-category-select").val()
      }
      $.ajax({
        url: 'https://localhost:7110/IncomeCategory',
        type: 'DELETE',
        success: function(data, status) {
          console.log("Data: " + data + "\nStatus: " + status)
          $('.modal-delete-income-category').fadeOut();

        },
        data: incomeCategory
      });
    } else {
      alert("Fill all the fields!");
    }
  });
}

function onDeleteExpenseCategoryInit() {
  $("#submit_delete_expense_category").click(function (e) {
    e.preventDefault();
    //$('.modal-create-category').fadeIn();*/
    if ($("#delete-expense-category-select").val() != ""){
      let expenseCategory = {
        userName: GodObj.userName,
        accountName: GodObj.accountName,
        categoryName: $("#delete-expense-category-select").val()
      }
      $.ajax({
        url: 'https://localhost:7110/ExpenseCategory',
        type: 'DELETE',
        success: function(data, status) {
          console.log("Data: " + data + "\nStatus: " + status)
          $('.modal-delete-expense-category').fadeOut();

        },
        data: expenseCategory
      });
    } else {
      alert("Fill all the fields!");
    }
  });
}

function onCreateExpenseStatementInit() {
  $("#submit_create_expense_statement").click(function (e) {
    e.preventDefault();
    if ($("#amount-create-expense-statement").val() != ""){
      let expenseStatement = {
        userName: GodObj.userName,
        accountName: GodObj.accountName,
        categoryName: $("#create-expense-statement-select").val(),
        amount: $("#amount-create-expense-statement").val()
      }
      $.post("https://localhost:7110/ExpenseStatement", expenseStatement, function( data, status ){
        console.log("Data: " + data + "\nStatus: " + status);
        if (status == "success") {
          $('.modal-create-expense-statement').fadeOut();
          $.get(`https://localhost:7110/Account?userName=${GodObj.userName}`, function(data){
            console.log(data);
            $(".profile-info").html(data.map((account) => account.name == GodObj.accountName &&
                `<div class="profile-user-name">User name:<span class="user-name-text">${account.userName}</span></div>\n` +
                `<div class="profile-account-name">Account name:<span class="account-name-text">${account.name}</span></div>\n` +
                `<div class="profile-balance">Balance:<span class="balance-number">${account.balance}</span> </div>`))
          })
        }
      });
    } else {
      alert("Fill all the fields!");
    }
  });
}

function onCreateIncomeStatementInit() {
  $("#submit_create_income_statement").click(function (e) {
    e.preventDefault();
    if ($("#amount-create-income-statement").val() != ""){
      let incomeStatement = {
        userName: GodObj.userName,
        accountName: GodObj.accountName,
        categoryName: $("#create-income-statement-select").val(),
        amount: $("#amount-create-income-statement").val()
      }
      $.post("https://localhost:7110/IncomeStatement", incomeStatement, function( data, status ){
        console.log("Data: " + data + "\nStatus: " + status);
        if (status == "success") {
          $('.modal-create-income-statement').fadeOut();
          $.get(`https://localhost:7110/Account?userName=${GodObj.userName}`, function(data){
            console.log(data);
            $(".profile-info").html(data.map((account) => account.name == GodObj.accountName &&
                `<div class="profile-user-name">User name:<span class="user-name-text">${account.userName}</span></div>\n` +
                `<div class="profile-account-name">Account name:<span class="account-name-text">${account.name}</span></div>\n` +
                `<div class="profile-balance">Balance:<span class="balance-number">${account.balance}</span> </div>`))
          })
        }

      });
    } else {
      alert("Fill all the fields!");
    }
  });
}

function onTransferBalanceInit() {
  $("#submit_transfer-balance").click(function (e) {
    e.preventDefault();
    if ($("#amount-transfer-balance").val() != ""){
      let accountTransfer = {
        userName: GodObj.userName,
        fromAccountName: GodObj.accountName,
        toAccountName: $("#transfer-balance-select").val(),
        amount: $("#amount-transfer-balance").val()
      }
      $.ajax({
        url: 'https://localhost:7110/Account/TransferBalance',
        type: 'PUT',
        success: function(data, response) {
          console.log("Data: " + data + "\nStatus: " + response)
          $('.modal-transfer-balance').fadeOut();
          $.get(`https://localhost:7110/Account?userName=${GodObj.userName}`, function(data){
            console.log(data);
            $(".profile-info").html(data.map((account) => account.name == GodObj.accountName &&
                `<div class="profile-user-name">User name:<span class="user-name-text">${account.userName}</span></div>\n` +
                `<div class="profile-account-name">Account name:<span class="account-name-text">${account.name}</span></div>\n` +
                `<div class="profile-balance">Balance:<span class="balance-number">${account.balance}</span> </div>`))
          })
        },
        data: accountTransfer
      });
    } else {
      alert("Fill all the fields!");
    }
  });
}

function onPaymentHistoryInit() {
  $("#balance-history-button").click(function () {
    $('.manage-buttons-wrapper').addClass('hidden')
    //$('.statement-list').removeClass('hidden')
    $('.history-buttons-wrapper').removeClass('hidden')

  });
}

function onIncomeStatementsHistoryInit() {
  $("#submit_income-statements-history").click(function (e) {
    e.preventDefault();
    if ($("#income-statements-history-select").val() != "No category"){
      let category = $("#income-statements-history-select").val()
      $.get(`https://localhost:7110/Account/ReceiveStatements?userName=${GodObj.userName}&accountName=${GodObj.accountName}&categoryName=${category}`, function(data, status){
        console.log(data);
        if (status == "success") {
          $('.modal-income-statements-history').fadeOut();
          $('.history-buttons-wrapper').addClass('hidden')
          $('.statement-list').removeClass('hidden')
          $('.statements-wrapper').removeClass('hidden')

          $(".statement-list").html(data.map((statement) =>
              '<div class="statement">\n' +
              `<p class="statement-date">${statement.dateTime}</p>\n` +
              `<p class="statement-amount">Amount: <span class="statement-amount-number">${statement.amount}</span></p>\n` +
              `<p class="statement-category">${statement.categoryName}</p>\n` +
              '</div>'))
        }
      });
    } else {
      $.get(`https://localhost:7110/Account/ReceiveStatements?userName=${GodObj.userName}&accountName=${GodObj.accountName}`, function(data, status){
        console.log(data);
        if (status == "success") {
          $('.modal-income-statements-history').fadeOut();
          $('.history-buttons-wrapper').addClass('hidden')
          $('.statement-list').removeClass('hidden')
          $('.statements-wrapper').removeClass('hidden')

          $(".statement-list").html(data.map((statement) =>
              '<div class="statement">\n' +
              `<p class="statement-date">${statement.dateTime}</p>\n` +
              `<p class="statement-amount">Amount: <span class="statement-amount-number">${statement.amount}</span></p>\n` +
              `<p class="statement-category">${statement.categoryName}</p>\n` +
              '</div>'))
        }
      });
    }
  });
}

function onExpenseStatementsHistoryInit() {
  $("#submit_expense-statements-history").click(function (e) {
    e.preventDefault();
    if ($("#expense-statements-history-select").val() != "No category"){
      let category = $("#expense-statements-history-select").val()
      $.get(`https://localhost:7110/Account/SpentStatements?userName=${GodObj.userName}&accountName=${GodObj.accountName}&categoryName=${category}`, function(data, status){
        console.log(data);
        if (status == "success") {
          $('.modal-expense-statements-history').fadeOut();
          $('.history-buttons-wrapper').addClass('hidden')
          $('.statement-list').removeClass('hidden')
          $('.statements-wrapper').removeClass('hidden')

          $(".statement-list").html(data.map((statement) =>
              '<div class="statement">\n' +
              `<p class="statement-date">${statement.dateTime}</p>\n` +
              `<p class="statement-amount">Amount: <span class="statement-amount-number">${statement.amount}</span></p>\n` +
              `<p class="statement-category">${statement.categoryName}</p>\n` +
              '</div>'))
        }
      });
    } else {
      $.get(`https://localhost:7110/Account/SpentStatements?userName=${GodObj.userName}&accountName=${GodObj.accountName}`, function(data, status){
        console.log(data);
        if (status == "success") {
          $('.modal-expense-statements-history').fadeOut();
          $('.history-buttons-wrapper').addClass('hidden')
          $('.statement-list').removeClass('hidden')
          $('.statements-wrapper').removeClass('hidden')

          $(".statement-list").html(data.map((statement) =>
              '<div class="statement">\n' +
              `<p class="statement-date">${statement.dateTime}</p>\n` +
              `<p class="statement-amount">Amount: <span class="statement-amount-number">${statement.amount}</span></p>\n` +
              `<p class="statement-category">${statement.categoryName}</p>\n` +
              '</div>'))
        }
      });
    }
  });
}

function onSpentAmountHistoryInit() {
  $("#submit_spent-amount-history").click(function (e) {
    e.preventDefault();
    if ($("#spent-amount-select").val() != "No category"){
      let category = $("#spent-amount-select").val()
      $.get(`https://localhost:7110/Account/SpentAmount?userName=${GodObj.userName}&accountName=${GodObj.accountName}&categoryName=${category}`, function(data, status){
        console.log(data);
        if (status == "success") {
          $(".modal-spent-amount-history").fadeOut();
          $(".modal-spent-amount-amount").fadeIn();
          $(".spent-num").html(data);
          //$('.modal-spent-amount-history').fadeOut();
        }
      });
    } else {
      $.get(`https://localhost:7110/Account/SpentAmount?userName=${GodObj.userName}&accountName=${GodObj.accountName}`, function(data, status){
        console.log(data);
        if (status == "success") {
          $(".modal-spent-amount-history").fadeOut();
          $(".modal-spent-amount-amount").fadeIn();
          $(".spent-num").html(data);
          //$('.modal-spent-amount-history').fadeOut();
        }
      });
    }
  });
}

function onIncomeAmountHistoryInit() {
  $("#submit_income-amount-history").click(function (e) {
    e.preventDefault();
    if ($("#income-amount-history-select").val() != "No category"){
      let category = $("#income-amount-history-select").val()
      $.get(`https://localhost:7110/Account/ReceivedAmount?userName=${GodObj.userName}&accountName=${GodObj.accountName}&categoryName=${category}`, function(data, status){
        console.log(data);
        if (status == "success") {
          /*$("#submit_income-amount-history").click(function () {
            $(".modal-income-amount-history").fadeOut()
            $(".modal-income-amount-amount").fadeIn()
          })*/
          $(".modal-income-amount-history").fadeOut();
          $(".modal-income-amount-amount").fadeIn();
          $(".earned-num").html(data);
          //$('.modal-income-amount-history').fadeOut();
        }
      });
    } else {
      $.get(`https://localhost:7110/Account/ReceivedAmount?userName=${GodObj.userName}&accountName=${GodObj.accountName}`, function(data, status){
        console.log(data);
        if (status == "success") {
          //alert(data)
          $(".modal-income-amount-history").fadeOut();
          $(".modal-income-amount-amount").fadeIn();
          $(".earned-num").html(data);
          //$('.modal-income-amount-history').fadeOut();
        }
      });
    }
  });
}

function modalCreateIncomeCategoryInit() {
  $('.modal-create-income-category').hide();
  $("#income-category-button").click(function () {
    //e.preventDefault();
    $('.modal-create-income-category').fadeIn();
  });
  $(".close").click(function () {
    $('.modal-create-income-category').fadeOut();
  });
}

function modalCreateExpenseCategoryInit() {
  $('.modal-create-expense-category').hide();
  $("#expense-category-button").click(function () {
    //e.preventDefault();
    $('.modal-create-expense-category').fadeIn();
  });
  $(".close").click(function () {
    $('.modal-create-expense-category').fadeOut();
  });
}

function modalCreateIncomeStatementInit() {
  $('.modal-create-income-statement').hide();
  $("#income-statement-button").click(function () {
    //e.preventDefault();
    $('.modal-create-income-statement').fadeIn();
    $.get(`https://localhost:7110/IncomeCategory?userName=${GodObj.userName}&accountName=${GodObj.accountName}`, function(data){
      console.log(data);
      $("#create-income-statement-select").html(data.map((category) => `<option>${category.categoryName}</option>`))
    });
  });
  $(".close").click(function () {
    $('.modal-create-income-statement').fadeOut();
  });
}

function modalCreateExpenseStatementInit() {
  $('.modal-create-expense-statement').hide();
  $("#expense-statement-button").click(function () {
    //e.preventDefault();
    $('.modal-create-expense-statement').fadeIn();
    $.get(`https://localhost:7110/ExpenseCategory?userName=${GodObj.userName}&accountName=${GodObj.accountName}`, function(data){
      console.log(data);
      $("#create-expense-statement-select").html(data.map((category) => `<option>${category.categoryName}</option>`))
    });
  });
  $(".close").click(function () {
    $('.modal-create-expense-statement').fadeOut();
  });
}

function modalDeleteIncomeCategoryInit() {
  $('.modal-delete-income-category').hide();
  $("#income-category-delete-button").click(function () {
    $('.modal-delete-income-category').fadeIn();

    $.get(`https://localhost:7110/IncomeCategory?userName=${GodObj.userName}&accountName=${GodObj.accountName}`, function(data){
      console.log(data);
      $("#delete-income-category-select").html(data.map((category) => category.categoryName != 'Default' && `<option>${category.categoryName}</option>`))
    });
  });
  $(".close").click(function () {
    $('.modal-delete-income-category').fadeOut();
  });
}

function modalTransferBalanceInit() {
  $('.modal-transfer-balance').hide();
  $("#transfer-balance-button").click(function () {
    $('.modal-transfer-balance').fadeIn();

    $.get(`https://localhost:7110/Account?userName=${GodObj.userName}`, function(data){
      console.log(data);
      $("#transfer-balance-select").html(data.map((account) => account.name != GodObj.accountName && `<option>${account.name}</option>`))
    });
  });
  $(".close").click(function () {
    $('.modal-transfer-balance').fadeOut();
  });
}

function modalDeleteExpenseCategoryInit() {
  $('.modal-delete-expense-category').hide();
  $("#expense-category-delete-button").click(function () {
    $('.modal-delete-expense-category').fadeIn();

    $.get(`https://localhost:7110/ExpenseCategory?userName=${GodObj.userName}&accountName=${GodObj.accountName}`, function(data){
      console.log(data);
      $("#delete-expense-category-select").html(data.map((category) => category.categoryName != 'Default' && `<option>${category.categoryName}</option>`))
    });
  });
  $(".close").click(function () {
    $('.modal-delete-expense-category').fadeOut();
  });
}

function modalHistorySpentAmountInit() {
  $('.modal-spent-amount-history').hide();
  $("#spent-amount-history-button").click(function () {
    //e.preventDefault();
    $('.modal-spent-amount-history').fadeIn();

    $.get(`https://localhost:7110/ExpenseCategory?userName=${GodObj.userName}&accountName=${GodObj.accountName}`, function(data){
      console.log(data);
      $("#spent-amount-select").html(data.map((category) => `<option>${category.categoryName}</option>`))
      let x = document.getElementById("spent-amount-select");
      let option = document.createElement("option");
      option.text = "No category";
      x.add(option, 0);
    });
  });
  $(".close").click(function () {
    $('.modal-spent-amount-history').fadeOut();
  });
}

function modalHistoryIncomeAmountInit() {
  $('.modal-income-amount-history').hide();
  $("#income-amount-history-button").click(function () {
    //e.preventDefault();
    $('.modal-income-amount-history').fadeIn();
    $.get(`https://localhost:7110/IncomeCategory?userName=${GodObj.userName}&accountName=${GodObj.accountName}`, function(data){
      console.log(data);
      $("#income-amount-history-select").html(data.map((category) => `<option>${category.categoryName}</option>`))
      let x = document.getElementById("income-amount-history-select");
      let option = document.createElement("option");
      option.text = "No category";
      x.add(option, 0);
    });
  });
  $(".close").click(function () {
    $('.modal-income-amount-history').fadeOut();
  });
}

function modalHistorySpentStatementsInit() {
  $('.modal-expense-statements-history').hide();
  $("#expense-statements-history-button").click(function () {
    //e.preventDefault();
    $('.modal-expense-statements-history').fadeIn();
    $.get(`https://localhost:7110/ExpenseCategory?userName=${GodObj.userName}&accountName=${GodObj.accountName}`, function(data){
      console.log(data);
      $("#expense-statements-history-select").html(data.map((category) => `<option>${category.categoryName}</option>`))
      let x = document.getElementById("expense-statements-history-select");
      let option = document.createElement("option");
      option.text = "No category";
      x.add(option, 0);
    });
  });
  $(".close").click(function () {
    $('.modal-expense-statements-history').fadeOut();
  });
}

function modalHistoryIncomeStatementsInit() {
  $('.modal-income-statements-history').hide();
  $("#income-statements-history-button").click(function () {
    //e.preventDefault();
    $('.modal-income-statements-history').fadeIn();
    $.get(`https://localhost:7110/IncomeCategory?userName=${GodObj.userName}&accountName=${GodObj.accountName}`, function(data){
      console.log(data);
      $("#income-statements-history-select").html(data.map((category) => `<option>${category.categoryName}</option>`))
      let x = document.getElementById("income-statements-history-select");
      let option = document.createElement("option");
      option.text = "No category";
      x.add(option, 0);
    });
  });
  $(".close").click(function () {
    $('.modal-income-statements-history').fadeOut();
  });
}

function onDeleteInit() {
  $(".delete-button").click(function () {
    let deleteUserName = $(this).attr('id');
    let deleteAccountName = $(this).parent().find('.account-name').text();

    let accountDelete = {
      //userName: deleteUserName,
      userName: GodObj.userName,
      accountName: deleteAccountName
    }

    $.ajax({
      url: 'https://localhost:7110/Account',
      type: 'DELETE',
      success: function() {
        $.get(`https://localhost:7110/Account?userName=${deleteUserName}`, function(data){
          console.log(data);
          $(".account-list").html(data.map((account) => '<div class="account">\n' +
              `        <p class="account-name">${account.name}</p>\n` +
              `        <p class="account-balance">balance:<span>${account.balance}</span></p>\n` +
              `        <button class="choose-button">Choose</button>\n` +
              `        <button id=${account.userName} class="delete-button" onclick="onDeleteInit()">Delete</button>\n` +
              '    </div>'))
        });
      },
      data: accountDelete
    });

  });
}

function onBackToAccountsInit() {
  $("#back-to-accounts-button").click(function () {

    $('.manage-buttons-wrapper').addClass('hidden')
    $('.profile-info').addClass('hidden')
    $('.accounts-wrapper').removeClass('hidden')
    $('.account-list').removeClass('hidden')
    $('.add').removeClass('hidden')
    $('.logout').removeClass('hidden')

    $.get(`https://localhost:7110/Account?userName=${GodObj.userName}`, function(data){
      console.log(data);
      $(".account-list").html(data.map((account) => '<div class="account">\n' +
          `        <p class="account-name">${account.name}</p>\n` +
          `        <p class="account-balance">balance:<span class="account-balance-number">${account.balance}</span></p>\n` +
          `        <button id=${account.name} class="choose-button" onclick="onChooseAccountInit()">Choose</button>\n` +
          `        <button id=${account.userName} class="delete-button" onclick="onDeleteInit()">Delete</button>\n` +
          '    </div>'))
    });
  });
}

function onBackToAccountOperationsInit() {
  $("#back-to-account-operations-button").click(function () {

    $('.history-buttons-wrapper').addClass('hidden')
    $('.manage-buttons-wrapper').removeClass('hidden')
  });
}

function onBackToPaymentHistoryInit() {
  $("#back-to-payment-history-button").click(function () {
    $('.history-buttons-wrapper').removeClass('hidden')
    $('.statement-list').addClass('hidden')
    $('.statements-wrapper').addClass('hidden')
  });
}

function onLogoutInit() {
  $(".logout").click(function () {
    /*$('.container').removeClass('hidden')
    $('.account-list').addClass('hidden')
    $('.add').addClass('hidden')
    $('.logout').addClass('hidden')*/
    location.reload();
  });
}

function onEarnedAmountInit() {
  $('.modal-income-amount-amount').hide();

  $(".close").click(function () {
    $('.modal-income-amount-amount').fadeOut();
  });
}

function onSpentAmountInit() {
  $('.modal-spent-amount-amount').hide();

  $(".close").click(function () {
    $('.modal-spent-amount-amount').fadeOut();
  });
}

function formsChangeInit() {
  $(".btn-log").click(function () {
    console.log("clicked");
    $(".btn-log").addClass("unactive");
    $(".register-form").addClass("unactive");

    $(".btn-reg").removeClass("unactive");
    $(".login-form").removeClass("unactive");
  });

  $(".btn-reg").click(function () {
    $(".btn-reg").addClass("unactive");
    $(".login-form").addClass("unactive");

    $(".btn-log").removeClass("unactive");
    $(".register-form").removeClass("unactive");
  });
}

$(document).ready(function () {
  onRegisterInit();
  onLoginInit();
  onDeleteInit();
  onChooseAccountInit();
  modalInit();
  modalCreateIncomeStatementInit();
  modalCreateExpenseStatementInit();
  modalCreateIncomeCategoryInit();
  modalCreateExpenseCategoryInit();
  modalDeleteIncomeCategoryInit();
  modalDeleteExpenseCategoryInit();
  modalTransferBalanceInit();
  modalHistorySpentAmountInit();
  modalHistoryIncomeAmountInit();
  modalHistorySpentStatementsInit();
  modalHistoryIncomeStatementsInit();
  onCreateAccountInit();
  onCreateIncomeCategoryInit();
  onCreateExpenseCategoryInit();
  onDeleteIncomeCategoryInit();
  onDeleteExpenseCategoryInit();
  onPaymentHistoryInit();
  formsChangeInit();
  onCreateExpenseStatementInit();
  onCreateIncomeStatementInit();
  onTransferBalanceInit();
  onIncomeStatementsHistoryInit();
  onExpenseStatementsHistoryInit();
  onIncomeAmountHistoryInit();
  onSpentAmountHistoryInit();
  onBackToAccountsInit();
  onBackToAccountOperationsInit();
  onBackToPaymentHistoryInit();
  onLogoutInit();
  onEarnedAmountInit();
  onSpentAmountInit();
});
