$.validator.methods.date = function (value, element) {
    return this.optional(element) ||
        Globalize.parseDate(value) ||
        Globalize.parseDate(value, "yyyy-MM-dd");
}