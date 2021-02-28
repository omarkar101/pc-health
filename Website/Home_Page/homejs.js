let sortDirection = false;
var num = 1
let datalst = [
    {
        username: 'user1', pc: 'on', health: 'healthy', OS: 'windows', CPU: 50,
        RAM: 10, disk: 37, contact: 'user1@gmail.com'
    },
    {
        username: 'user3', pc: 'on', health: 'healthy', OS: 'Linux', CPU: 0,
        RAM: 0, disk: 7, contact: 'user3@gmail.com'
    },
    {
        username: 'user4', pc: 'on', health: 'in danger', OS: 'Windows', CPU: 1,
        RAM: 100, disk: 20, contact: 'user4@gmail.com'
    },
    {
        username: 'user1', pc: 'on', health: 'healthy', OS: 'windows', CPU: 50,
        RAM: 10, disk: 37, contact: 'user1@gmail.com'
    },
    {
        username: 'user3', pc: 'on', health: 'healthy', OS: 'Linux', CPU: 0,
        RAM: 0, disk: 7, contact: 'user3@gmail.com'
    },
    {
        username: 'user4', pc: 'on', health: 'in danger', OS: 'Windows', CPU: 1,
        RAM: 100, disk: 20, contact: 'user4@gmail.com'
    },
    {
        username: 'user5', pc: 'on', health: 'Critical', OS: 'windows', CPU: 50,
        RAM: 10, disk: 37, contact: 'user1@gmail.com'
    },
    {
        username: 'user3', pc: 'on', health: 'healthy', OS: 'Linux', CPU: 0,
        RAM: 0, disk: 7, contact: 'user3@gmail.com'
    },
    {
        username: 'user4', pc: 'on', health: 'in danger', OS: 'Windows', CPU: 1,
        RAM: 100, disk: 20, contact: 'user4@gmail.com'
    },
    {
        username: 'user1', pc: 'on', health: 'healthy', OS: 'windows', CPU: 50,
        RAM: 10, disk: 37, contact: 'user1@gmail.com'
    },
    {
        username: 'user3', pc: 'on', health: 'healthy', OS: 'Linux', CPU: 0,
        RAM: 0, disk: 7, contact: 'user3@gmail.com'
    }];

window.onload = () => {
    addToTable(datalst);
};

function addToTable(datalst) {
    const tablebody = document.getElementById('tableData');
    let datastr = '';
    for (let x of datalst) {
        datastr += `<tr  onclick = "showHideRow('hidden_row${num}')"><td>${x.username}</td><td>${x.pc}</td><td>${x.health}</td><td>${x.OS}</td><td>${x.CPU}%</td><td>${x.RAM}</td><td>${x.disk}</td><td>${x.contact}</td></tr>
        <tr style="display:none; background-color: #9dcfccda;"  id="hidden_row${num}"><td colspan="8"> PC ${x.username} works on ${x.OS} operating system. The PC is ${x.health} condition.</tr>`;
        num++
    }
    tablebody.innerHTML = datastr;
}

function showHideRow(row) {
    $("#" + row).toggle();
}


function sortByColumn(colname) {
    const dataType = typeof datalst[0][colname];
    sortDirection = !sortDirection;
    switch (dataType) {
        case 'number':
            SortByNumber(sortDirection, colname);
            break;
        case 'string':
            SortByString(sortDirection, colname);
            break;
    }
    console.log(datalst)
    addToTable(datalst)
}

function SortByNumber(sort, colname) {
    datalst = datalst.sort((p1, p2) => {
        return sort ? p1[colname] - p2[colname] : p2[colname] - p1[colname]
    })
}

function SortByString(sort, colname) {
    datalst.sort((a, b) => {
        var nameA = a[colname].toUpperCase();
        var nameB = b[colname].toUpperCase();
        if (nameA < nameB) {
            if (sort) {
                return -1;
            }
            else {
                return 1;
            }
        }
        if (nameA > nameB) {
            if (sort) {
                return 1;
            }
            else {
                return -1;
            }
        }
    })
}

function showColumns() {
    $("input:checkbox").attr("checked", false).click(function () {
        var showcolumn = "." + $(this).attr("name");
        $(showcolumn).toggle();
    });
    document.getElementById('settings').style.display = "none";
}

$(document).ready(function () {
    $('#search-input').keyup(function () {
        search_table($(this).val());
    });
    function search_table(value) {
        $('#tableData tr').each(function () {
            var found = 'false';
            $(this).each(function () {
                if ($(this).find("td:first").text().toLowerCase().indexOf(value.toLowerCase()) >= 0) {
                    found = 'true';
                }
            });
            if (found == 'true') {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        });
    }
});