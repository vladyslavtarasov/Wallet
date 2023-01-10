console.log("started");

let user = {
  name: "vlad",
  surname: "tarasov",
  userName: "vladt",
  password: "vladt",
};

let userLogin = {
  userName: "vladt",
  password: "vladt",
};

let accountCreate = {
  userName: "vladt",
  balance: 100,
  accountName: "account1"
}

let accountDelete = {
  userName: "vladt",
  accountName: "account1"
}

let accountTransfer = {
  userName: "vladt",
  fromAccountName: "account",
  toAccountName: "account1",
  amount: 10
}

let expenseCategory = {
  userName: "vladt",
  accountName: "account",
  categoryName: "expenseCategory"
}

let expenseCategoryDelete = {
  userName: "vladt",
  accountName: "account",
  categoryName: "expenseCategory"
}

let incomeCategory = {
  userName: "vladt",
  accountName: "account",
  categoryName: "incomeCategory"
}

let incomeCategoryDelete = {
  userName: "vladt",
  accountName: "account",
  categoryName: "incomeCategory"
}

let expenseStatement = {
  userName: "vladt",
  accountName: "account",
  categoryName: "expenseCategory",
  amount: 50
}

let incomeStatement = {
  userName: "vladt",
  accountName: "account",
  categoryName: "incomeCategory",
  amount: 50
}
  
/*$.get("https://localhost:7110/ExpenseCategory?userName=Q&accountName=Q", function(data){
    console.log(data);
});*/

/*$.get("https://localhost:7110/ExpenseCategory?userName=Q&accountName=Q", function(data){
  console.log(data);
}, "json");*/


//Account


/*$.post("https://localhost:7110/Account", accountCreate, function( data, status ){
  console.log("Data: " + data + "\nStatus: " + status);
});*/

/*$.ajax({
  url: 'https://localhost:7110/Account',
  type: 'DELETE',
  success: function(data, status) {
    console.log("Data: " + data + "\nStatus: " + status)
  },
  data: accountDelete
});*/

/*$.get("https://localhost:7110/Account?userName=vladt", function(data){
    console.log(data);
});*/

/*$.post("https://localhost:7110/Account/TransferBalance", accountTransfer, function( data, status ){
  console.log("Data: " + data + "\nStatus: " + status);
});*/

/*$.get("https://localhost:7110/Account/SpentAmount?userName=vladt&accountName=account&categoryName=Default", function(data){
    console.log(data);
});*/

/*$.get("https://localhost:7110/Account/SpentAmount?userName=vladt&accountName=account", function(data){
    console.log(data);
});*/

/*$.get("https://localhost:7110/Account/ReceivedAmount?userName=vladt&accountName=account&categoryName=Default", function(data){
    console.log(data);
});*/

/*$.get("https://localhost:7110/Account/ReceivedAmount?userName=vladt&accountName=account", function(data){
    console.log(data);
});*/

/*$.get("https://localhost:7110/Account/SpentStatements?userName=vladt&accountName=account&categoryName=Default", function(data){
    console.log(data);
});*/

/*$.get("https://localhost:7110/Account/SpentStatements?userName=vladt&accountName=account", function(data){
    console.log(data);
});*/

/*$.get("https://localhost:7110/Account/ReceiveStatements?userName=vladt&accountName=account&categoryName=Default", function(data){
    console.log(data);
});*/

/*$.get("https://localhost:7110/Account/ReceiveStatements?userName=vladt&accountName=account", function(data){
    console.log(data);
});*/


//ExpenseCategory


/*$.post("https://localhost:7110/ExpenseCategory", expenseCategory, function( data, status ){
  console.log("Data: " + data + "\nStatus: " + status);
});*/

/*$.ajax({
  url: 'https://localhost:7110/ExpenseCategory',
  type: 'DELETE',
  success: function(data, status) {
    console.log("Data: " + data + "\nStatus: " + status)
  },
  data: expenseCategoryDelete
});*/

/*$.get("https://localhost:7110/ExpenseCategory?userName=vladt&accountName=account", function(data){
  console.log(data);
});*/


//ExpenseStatement


/*$.post("https://localhost:7110/ExpenseStatement", expenseStatement, function( data, status ){
  console.log("Data: " + data + "\nStatus: " + status);
})*/


//IncomeCategory


/*$.post("https://localhost:7110/IncomeCategory", incomeCategory, function( data, status ){
  console.log("Data: " + data + "\nStatus: " + status);
});*/

/*$.ajax({
  url: 'https://localhost:7110/IncomeCategory',
  type: 'DELETE',
  success: function(data, status) {
    console.log("Data: " + data + "\nStatus: " + status)
  },
  data: incomeCategoryDelete
});*/

/*$.get("https://localhost:7110/IncomeCategory?userName=vladt&accountName=account", function(data){
  console.log(data);
});*/


//IncomeStatement


/*$.post("https://localhost:7110/IncomeStatement", incomeStatement, function( data, status ){
  console.log("Data: " + data + "\nStatus: " + status);
})*/


//User


/*$.post("https://localhost:7110/User/Register", user);*/

/*$.post("https://localhost:7110/User/Login", userLogin, function( data ){
    console.log(data);
});*/



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
      console.log(new_user);
      /*let status = login(new_user).then();
      console.log(status);*/
      $.post("https://localhost:7110/User/Login", new_user, function (data, status) {
        console.log("Data: " + data + "\nStatus: " + status);
        if (status == "success") {
          $('.container').addClass('hidden')
          $('.account-list').removeClass('hidden')
          
          $.get(`https://localhost:7110/Account?userName=${new_user.userName}`, function(data){
            console.log(data);
            $(".account-list").html(data.map((account) => '<div class="account">\n' +
                `        <p class="account-name">${account.name}</p>\n` +
                `        <p class="account-balance">balance:<span>${account.balance}</span></p>\n` +
                `        <button class="choose-button">Choose</button>\n` +
                `        <button id=${account.userName} class="delete-button" onclick="onDeleteInit()">Delete</button>\n` +
                '    </div>'))
          });
        }
      });
      //location.replace('../wwwroot/accountsMenu.html')
    } else {
      alert("Fill all the fields!");
    }
  });
}

function onCreateAccountInit() {
  $("#submit_create_account").click(function (e) {
    e.preventDefault();
    if ($("#user_name_create_account").val() != "" && $("#account_name_create_account").val() != "" 
        && $("#balance_create_account").val() != ""){
      let new_account = {
        userName: $("#user_name_create_account").val(),
        balance: $("#balance_create_account").val(),
        accountName: $("#account_name_create_account").val()
      };
      console.log(new_account);
      
      $.post("https://localhost:7110/Account", new_account, function (data, status) {
        console.log("Data: " + data + "\nStatus: " + status);
        if (status == "success") {
          $('.modal').fadeOut();
          $.get(`https://localhost:7110/Account?userName=${new_account.userName}`, function(data){
            console.log(data);
            $(".account-list").html(data.map((account) => '<div class="account">\n' +
                `        <p class="account-name">${account.name}</p>\n` +
                `        <p class="account-balance">balance:<span>${account.balance}</span></p>\n` +
                `        <button class="choose-button">Choose</button>\n` +
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
      console.log(new_user);
      //var result = register(new_user);
      $.post("https://localhost:7110/User/Register", new_user, function (data, status) {
        console.log("Data: " + data + "\nStatus: " + status);
        if (status == "success") {
          $('.container').addClass('hidden')
          $('.account-list').removeClass('hidden')

          $.get(`https://localhost:7110/Account?userName=${new_user.userName}`, function(data){
            console.log(data);
            $(".account-list").html(data.map((account) => '<div class="account">\n' +
                `        <p class="account-name">${account.name}</p>\n` +
                `        <p class="account-balance">balance:<span>${account.balance}</span></p>\n` +
                `        <button class="choose-button">Choose</button>\n` +
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

function modalInit() {
  $('.modal').hide();
  $(".add").click(function () {
    $('.modal').fadeIn();
  });
  $(".close").click(function () {
    $('.modal').fadeOut();
  });
}

function onDeleteInit() {
  $(".delete-button").click(function () {
    let deleteUserName = $(this).attr('id');
    let deleteAccountName = $(this).parent().find('.account-name').text();

    let accountDelete = {
      userName: deleteUserName,
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
  modalInit();
  onCreateAccountInit();
  formsChangeInit();
});
