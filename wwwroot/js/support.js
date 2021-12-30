$(document).ready(function () {
    $('.wrap').on('keyup', 'textarea', function (e) {
        $(this).css('height', 'auto');
        $(this).height(this.scrollHeight);
    });
    $('.wrap').find('textarea').keyup();

    $('input[type="text"]').keydown(function () {
        if (event.keyCode === 13) {
            event.preventDefault();
        };
    });

    var textCountLimit = 1000;

    $('textarea[id=questionsanswervwriteviewmodel_Content]').keyup(function () {
        // 텍스트영역의 길이를 체크
        var textLength = $(this).val().length;

        var str = textCountLimit + ' / ' + textLength
        // 입력된 텍스트 길이를 #textCount 에 업데이트 해줌
        $('#textCount').text(str);

        // 제한된 길이보다 입력된 길이가 큰 경우 제한 길이만큼만 자르고 텍스트영역에 넣음
        if (textLength > textCountLimit) {
            $(this).val($(this).val().substr(0, textCountLimit));
        }
    });

});