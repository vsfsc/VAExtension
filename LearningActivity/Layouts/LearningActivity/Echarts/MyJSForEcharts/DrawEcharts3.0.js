//echarts3.0有许多不足，如字乱码，legend不现实，异步执行支持度低
//theme
var colorPalette = [
        '#2ec7c9', '#b6a2de', '#5ab1ef', '#ffb980', '#d87a80',
        '#8d98b3', '#e5cf0d', '#97b552', '#95706d', '#dc69aa',
        '#07a2a4', '#9a7fd1', '#588dd5', '#f5994e', '#c05050',
        '#59678c', '#c9ab00', '#7eb00a', '#6f5553', '#c14089'
];
var theme = {
    color: colorPalette,

    title: {
        textStyle: {
            fontWeight: 'normal',
            color: '#008acd'
        }
    },

    visualMap: {
        itemWidth: 15,
        color: ['#5ab1ef', '#e0ffff']
    },

    toolbox: {
        iconStyle: {
            normal: {
                borderColor: colorPalette[0]
            }
        }
    },

    tooltip: {
        backgroundColor: 'rgba(50,50,50,0.5)',
        axisPointer: {
            type: 'line',
            lineStyle: {
                color: '#008acd'
            },
            crossStyle: {
                color: '#008acd'
            },
            shadowStyle: {
                color: 'rgba(200,200,200,0.2)'
            }
        }
    },

    dataZoom: {
        dataBackgroundColor: '#efefff',
        fillerColor: 'rgba(182,162,222,0.2)',
        handleColor: '#008acd'
    },

    grid: {
        borderColor: '#eee'
    },

    categoryAxis: {
        axisLine: {
            lineStyle: {
                color: '#008acd'
            }
        },
        splitLine: {
            lineStyle: {
                color: ['#eee']
            }
        }
    },

    valueAxis: {
        axisLine: {
            lineStyle: {
                color: '#008acd'
            }
        },
        splitArea: {
            show: true,
            areaStyle: {
                color: ['rgba(250,250,250,0.1)', 'rgba(200,200,200,0.1)']
            }
        },
        splitLine: {
            lineStyle: {
                color: ['#eee']
            }
        }
    },

    timeline: {
        lineStyle: {
            color: '#008acd'
        },
        controlStyle: {
            normal: { color: '#008acd' },
            emphasis: { color: '#008acd' }
        },
        symbol: 'emptyCircle',
        symbolSize: 3
    },

    line: {
        smooth: true,
        symbol: 'emptyCircle',
        symbolSize: 3
    },

    candlestick: {
        itemStyle: {
            normal: {
                color: '#d87a80',
                color0: '#2ec7c9',
                lineStyle: {
                    color: '#d87a80',
                    color0: '#2ec7c9'
                }
            }
        }
    },

    scatter: {
        symbol: 'circle',
        symbolSize: 4
    },

    map: {
        label: {
            normal: {
                textStyle: {
                    color: '#d87a80'
                }
            }
        },
        itemStyle: {
            normal: {
                borderColor: '#eee',
                areaColor: '#ddd'
            },
            emphasis: {
                areaColor: '#fe994e'
            }
        }
    },

    graph: {
        color: colorPalette
    },

    gauge: {
        axisLine: {
            lineStyle: {
                color: [[0.2, '#2ec7c9'], [0.8, '#5ab1ef'], [1, '#d87a80']],
                width: 10
            }
        },
        axisTick: {
            splitNumber: 10,
            length: 15,
            lineStyle: {
                color: 'auto'
            }
        },
        splitLine: {
            length: 22,
            lineStyle: {
                color: 'auto'
            }
        },
        pointer: {
            width: 5
        }
    }
};

echarts.registerTheme('macarons', theme);//主题注册



//为饼图设置Option
var data_pie_legend = [];
var data_pie_y1 = [];
var data_pie_y2 = [];
var echartTitle4Pie = '';
var echartID4Pie = '';
function setPieOption(echartID4Pie, echartTitle4Pie, data_pie_legend, data_pie_y1, data_pie_y2) {
    option_pie = {
        title: {
            text: echartTitle4Pie,//分别按照地点和类别对活动进行统计：记录数和总时长
            subtext: '南丁格尔玫瑰图(近十天统计)',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            x: 'center',
            y: 'bottom',
            //data: ['rose1', 'rose2', 'rose3', 'rose4', 'rose5', 'rose6', 'rose7', 'rose8']
            data: data_pie_legend
        },
        toolbox: {
            show: true,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: {
                    show: true,
                    type: ['pie', 'funnel']
                },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        series: [
            {
                name: '活动记录数',
                type: 'pie',
                radius: [20, 110],
                center: ['25%', 200],
                roseType: 'radius',
                label: {
                    normal: {
                        show: false
                    },
                    emphasis: {
                        show: true
                    }
                },
                lableLine: {
                    normal: {
                        show: false
                    },
                    emphasis: {
                        show: true
                    }
                },
                //data: [
                //    { value: 10, name: 'rose1' },
                //    { value: 5, name: 'rose2' },
                //    { value: 15, name: 'rose3' },
                //    { value: 25, name: 'rose4' },
                //    { value: 20, name: 'rose5' },
                //    { value: 35, name: 'rose6' },
                //    { value: 30, name: 'rose7' },
                //    { value: 40, name: 'rose8' }
                //]
                data: data_pie_y1
            },
            {
                name: '活动总时长',
                type: 'pie',
                radius: [30, 110],
                center: ['75%', 200],
                roseType: 'area',
                //data: [
                //    { value: 10, name: 'rose1' },
                //    { value: 5, name: 'rose2' },
                //    { value: 15, name: 'rose3' },
                //    { value: 25, name: 'rose4' },
                //    { value: 20, name: 'rose5' },
                //    { value: 35, name: 'rose6' },
                //    { value: 30, name: 'rose7' },
                //    { value: 40, name: 'rose8' }
                //]
                data: data_pie_y2
            }
        ]
    };
    //初始化图表
    var MyEchartBar = echarts.init(document.getElementById(echartID4Pie), 'macarons');
    MyEchartBar.setOption(option_pie);
    MyEchartBar.refresh();

}
//var MyEchartBar;
//编写带参的setOption函数
//为柱图设置option
var data_bar_x = [];
var data_bar_y1 = [];
var data_bar_y2 = [];
var data_bar_y3 = [];
var data_bar_y4 = [];
var echartTitle = '';

function setBarOption(echartID, echartTitle, data_bar_x, data_bar_y1, data_bar_y2, data_bar_y3, data_bar_y4) {
   
    option_bar = {
        title: {
            text: echartTitle,
            subtext: '柱图/折线'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['个人记录数', '总体记录数','个人时长','总体时长']
        },
        toolbox: {
            show: true,
            feature: {
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        xAxis: [
            {
                type: 'category',
                data: data_bar_x
                //data:[""]
            }
        ],
        yAxis: [
            {
                type: 'value',
                name: '记录数',
                axisLabel: {
                    formatter: '{value} 次'
                }
            },
            {
                type: 'value',
                name: '时长',
                axisLabel: {
                    formatter: '{value} min'
                }
            }
        ],
        series: [
            {
                name: '个人记录数',
                type: 'bar',
                //data: [5, 9, 7, 3, 6, 7, 6],
                data:data_bar_y1,
                markPoint: {
                    data: [
                        { type: 'max', name: '天最高' },
                        { type: 'min', name: '天最低' }
                    ]
                },
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
                }
            },
            {
                name: '总体记录数',
                type: 'bar',
                //data: [2.6, 5.9, 9.0, 26.4, 28.7, 70.7, 175.6, 182.2, 48.7, 18.8, 6.0, 2.3],
                data: data_bar_y2,
                markPoint: {
                    data: [
                        { type: 'max', name: '天最高' },
                        { type: 'min', name: '天最低' }
                    ]
                },
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
                }
            },
            {
                name: '个人时长',
                type: 'line',
                yAxisIndex: 1,
                data: data_bar_y3,
                markPoint: {
                    data: [
                        { type: 'max', name: '天最高' },
                        { type: 'min', name: '天最低' }
                    ]
                },
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
                }
            },
            {
                name: '总体时长',
                type: 'line',
                yAxisIndex: 1,
                data: data_bar_y4,
                markPoint: {
                    data: [
                        { type: 'max', name: '天最高' },
                        { type: 'min', name: '天最低' }
                    ]
                },
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
                }
            }
        ]
    };
    //初始化图表
    var MyEchartBar = echarts.init(document.getElementById(echartID), 'macarons');
    MyEchartBar.setOption(option_bar);
    MyEchartBar.refresh();
}

//使用AJAX向echart传输数据

var byType_btn_showFlag = false;
function ByType_Click() {
    if (byType_btn_showFlag == false) {
        byType_btn_showFlag = true;
        $("#type_pie").css({ display: 'block' });

        echartTitle4Pie = '按类型统计活动';
        echartID4Pie = 'type_pie';
        $.ajax({
            type: "post",
            async: false, //同步执行
            url: "LA_Main.aspx?type=TypePieData",
            dataType: "json", //返回数据形式为json
            success: function (result_json) {
                //alert("成功:" + result_json.legend[0]);
                //alert(result_json.category[0]);
                //alert(result_json[0].demoData);//输出：This Is The JSON Data         
                if (result_json) {
                    data_pie_legend = result_json.legend;
                    data_pie_y1 = result_json.myCounts;
                    data_pie_y2 = result_json.myDuration;
                    setPieOption(echartID4Pie, echartTitle4Pie, data_pie_legend, data_pie_y1, data_pie_y2);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("AJAX网页错误：" + errorThrown);
            }
        });
    }
    else {
        byType_btn_showFlag = false;
        $("#type_pie").css({ display: 'none' });
    }
    
}


var byLocation_btn_showFlag = false;
function ByLocation_Click() {
    if (byLocation_btn_showFlag == false) {
        byLocation_btn_showFlag = true;
        $("#location_pie").css({ display: 'block' });

        echartTitle4Pie = '按地点统计活动';
        echartID4Pie = 'location_pie';
        $.ajax({
            type: "post",
            async: false, //同步执行
            url: "LA_Main.aspx?type=LocationPieData",
            dataType: "json", //返回数据形式为json
            success: function (result_json) {
                //alert("成功:" + result_json.legend[0]);
                //alert(result_json.category[0]);
                //alert(result_json[0].demoData);//输出：This Is The JSON Data         
                if (result_json) {
                    data_pie_legend = result_json.legend;
                    data_pie_y1 = result_json.myCounts;
                    data_pie_y2 = result_json.myDuration;
                    setPieOption(echartID4Pie, echartTitle4Pie, data_pie_legend, data_pie_y1, data_pie_y2);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("AJAX网页错误：" + errorThrown);
            }
        });
    }
    else {
        byLocation_btn_showFlag = false;
        $("#location_pie").css({ display: 'none' });
    }
    
}



var week_btn_showFlag = false;
function AWeek_Click() {
    if (week_btn_showFlag == false) {
        week_btn_showFlag = true;
        $("#echartID_bar").css({ display: 'block' });

        echartTitle = '近7天活动统计';
        echartID = 'echartID_bar';
        $.ajax({
            type: "post",
            async: false, //同步执行
            url: "LA_Main.aspx?type=AWeekBarData",
            dataType: "json", //返回数据形式为json
            success: function (result_json) {
                //alert(result_json.category[0]);
                //alert(result_json[0].demoData);//输出：This Is The JSON Data         
                if (result_json) {
                    data_bar_x = result_json.category;
                    data_bar_y1 = result_json.myCounts;
                    data_bar_y2 = result_json.totalCounts;
                    data_bar_y3 = result_json.myDuration;
                    data_bar_y4 = result_json.totalDuration;
                    setBarOption(echartID, echartTitle, data_bar_x, data_bar_y1, data_bar_y2, data_bar_y3, data_bar_y4);

                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("AJAX网页错误：" + errorThrown);
            }
        });
    }
    else {
        week_btn_showFlag = false;
        $("#echartID_bar").css({ display: 'none' });
    }
    
}

var month_btn_showFlag = false;
function AMonth_Click() {
    if (month_btn_showFlag == false) {
        month_btn_showFlag = true;
        $("#month_bar").css({ display: 'block' });

        echartTitle = '近30天活动统计';
        echartID = 'month_bar';
        $.ajax({
            type: "post",
            async: false, //同步执行
            url: "LA_Main.aspx?type=AMonthBarData",
            dataType: "json", //返回数据形式为json
            success: function (result_json) {
                //alert(result_json.category[0]);
                //alert(result_json[0].demoData);//输出：This Is The JSON Data         
                if (result_json) {
                    data_bar_x = result_json.category;
                    data_bar_y1 = result_json.myCounts;
                    data_bar_y2 = result_json.totalCounts;
                    data_bar_y3 = result_json.myDuration;
                    data_bar_y4 = result_json.totalDuration;
                    setBarOption(echartID, echartTitle, data_bar_x, data_bar_y1, data_bar_y2, data_bar_y3, data_bar_y4);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("AJAX网页错误：" + errorThrown);
            }
        });
    }
    else {
        month_btn_showFlag = false;
        $("#month_bar").css({ display: 'none' });
    }
    

}

//全局变量displayFlag
var custom_btn_showFlag = false;
function Custom_Click() {
    if (custom_btn_showFlag == false) {
        $("#timect").css({ display: 'block' });
        $("#custom_bar").css({ display: 'block' });
        custom_btn_showFlag = true;

        var dateRange = new pickerDateRange('date1', {
            isTodayValid: true,
            needCompare: false,
            defaultText: ' 至 ',
            autoSubmit: true,
            inputTrigger: 'input_trigger1',
            theme: 'ta',
            success: function (obj) {               
                //AJAX将用户所选时间传到后台
                //______________ajax___________________________
                var str1 = obj.startDate;
                var str2 = obj.endDate;
                var title = str1 + "至" + str2 + "活动统计";
                echartTitle = title;
                echartID = 'custom_bar';
                $.ajax({
                    type: "post",
                    url: "LA_Main.aspx?type=CustomData",
                    data: { "startDate": str1, "endDate": str2 },
                    dataType: "json", //返回数据形式为json
                    success: function (result_json) {
                        if (result_json) {

                            data_bar_x = result_json.category;
                            data_bar_y1 = result_json.myCounts;
                            data_bar_y2 = result_json.totalCounts;
                            data_bar_y3 = result_json.myDuration;
                            data_bar_y4 = result_json.totalDuration;
                            setBarOption(echartID, echartTitle, data_bar_x, data_bar_y1, data_bar_y2, data_bar_y3, data_bar_y4);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("AJAX网页错误：" + errorThrown);
                    }
                });
                //_____________________________________________
            }
        });
    }
    else {
        $("#timect").css({ display: 'none' });
        $("#custom_bar").css({ display: 'none' });
        custom_btn_showFlag = false;
    }      
}


//$(document).ready(function () {
    
//    ////重新显示图表
//    //AWeek_Click();
//    //AMonth_Click();
//    //Custom_Click();

//});
