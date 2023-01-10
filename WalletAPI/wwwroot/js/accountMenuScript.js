console.log(userName)

//userName = 'vladt'

$.get(`https://localhost:7110/Account?userName=${userName}`, function(data){
    console.log(data);
    $(".accounts-wrapper").html(data.map((account) => '<div class="account">\n' +
        `        <p class="account-name">${account.name}</p>\n` +
        `        <p class="account-balance">balance:<span>${account.balance}</span></p>\n` +
        '        <button class="choose-button">Choose</button>\n' +
        '        <button class="delete-button">Delete</button>\n' +
        '    </div>'))
});

