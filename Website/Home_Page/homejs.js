/**
 * @var {bool} sortDirection  Will indicate if the sorting is ascending or descending
 * @var {int} num  Will refer to the number of users
 */
let sortDirection = false;
var num = 1

/**
 * @var {string} url Will refer to the link that is used to get HTTP request
 */
let url = 'https://pchealth.azurewebsites.net/api/Base/DiagnosticData';

/**
 * @var xhReq is used to get the HTTP request
 * @var datalst is used to store the json file retreieved from the server
 */
var xhReq = new XMLHttpRequest();
xhReq.open("GET", url, false);
xhReq.send(null);
var datalst = [JSON.parse(xhReq.responseText)];
/**
 * Used to call addToTable function to show it on the page
 */
window.onload = () => {
    addToTable(datalst);
};

/**
 * Adds PC information to the table of the Home Page
 * @param {json file} datalst the json file recieved from the server
 */
function addToTable(datalst) {
    let user = 1;
    const tablebody = document.getElementById('tableData');
    let datastr = '';
    for (let x of datalst[0]) {
        datastr += `<tr  onclick = "showHideRow('hidden_row${num}')"><td>${'user-' + x.PC_ID.slice(0,4)}</td><td>${x.OS}</td><td>${x.CpuUsage.toPrecision(5)}%</td><td>${x.TotalFreeDiskSpace.toPrecision(5)} GB</td><td>${x.MemoryUsage.toPrecision(4)} %</td><td>${x.AvgNetworkBytesSent.toPrecision(12)}</td><td>${x.AvgNetworkBytesReceived.toPrecision(12)}</td><td>${'user' + x.PC_ID.slice(0,4)+ '@gmail.com'}</td></tr>
        <tr style="display:none; background-color: #9dcfccda; white-space:pre-wrap; word-wrap:break-word;"  id="hidden_row${num}"><td colspan="8">PC's ID: ${x.PC_ID} 
        Total Disk Space= ${x.DiskTotalSpace} GB </tr>`;
        num++
        user++
    }
    tablebody.innerHTML = datastr;
}
/**
 * Add a hidden row under a specific row of a table.
 * @param {string} row 
 */
function showHideRow(row) {
    $("#" + row).toggle();
}
/**
 * Sorts the table according to a specific coloumn by clicking on the coloumn
 * @param {string} colname 
 */
function sortByColumn(colname) {
    datalst = datalst[0];
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
    datalst = [datalst];
    addToTable(datalst)
}
/**
 * Sorts the table according to a specific value, for example according to MemmoryUsage or Cpu-Usage
 * @param {var} sort 
 * @param {string} colname 
 */
function SortByNumber(sort, colname) {
    datalst = datalst.sort((p1, p2) => {
        return sort ? p1[colname] - p2[colname] : p2[colname] - p1[colname]
    })
}
/**
 * Sorts the table according to username
 * @param {var} sort 
 * @param {string} colname 
 */
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

/**
 * This function allows the user to input a search value for a specific username and show the user's row
 */
$(document).ready(function () {
    var count = 0;
    $('#search-input').keyup(function () {
        search_table($(this).val());
    });
    /**
     * Searches a table for a given value
     * @param {var} value 
     */
    function search_table(value) {
        $('#tableData tr').each(function () {
            var found = 'false';
            $(this).each(function () {
                if ($(this).find("td:first").text().toLowerCase().indexOf(value.toLowerCase()) >= 0) {
                    found = 'true';
                }
            });
            if (count % 2 == 0) {
                if (found == 'true') {
                    $(this).show();
                }
                else {
                    $(this).hide();//if a row was one of the hidden rows, it gets ignored
                }
            }
            else{
                $(this).hide();
            }
            count++
        });
    }
});