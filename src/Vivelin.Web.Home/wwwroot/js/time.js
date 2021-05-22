(function () {
  const units = {
    day: 1000 * 60 * 60 * 24,
    hour: 1000 * 60 * 60,
    minute: 1000 * 60
  }

  /**
   * Returns a string describing the approximate amount of elapsed time since the specified date.
   * @param {Date} date
   */
  function getElapsedTimeString(date) {
    const formatter = new Intl.RelativeTimeFormat(undefined, {
      numeric: 'auto',
      style: 'long'
    });

    const elapsedMs = Date.now() - date;
    for (const unit in units) {
      const treshold = units[unit];
      if (elapsedMs > treshold) {
        const value = Math.round(elapsedMs / treshold);
        return formatter.format(-value, unit)
      }
    }

    return 'just now';
  }

  function canPostProcess() {
    return Intl && Intl.RelativeTimeFormat && Intl.DateTimeFormat;
  }

  function postProcess() {
    const dateFormatter = new Intl.DateTimeFormat(undefined, {
      dateStyle: 'long',
      timeStyle: 'long'
    });

    for (const time of document.getElementsByTagName('time')) {
      if (time.dateTime) {
        var date = Date.parse(time.dateTime);
        time.innerText = getElapsedTimeString(date);
        time.title = dateFormatter.format(date);
      }
    }
  }

  if (canPostProcess()) {
    postProcess();
    window.setInterval(postProcess, 10000);
  }
}());