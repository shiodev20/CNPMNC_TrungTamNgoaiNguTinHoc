import { createStore } from "vuex";
import acount from "@/store/acount_data";
import branch from "@/store/branch_data";
import languages from "@/store/languages_data";
import certificate from "@/store/certificate_data";
import content_exame from "@/store/contentexame_data";
import course from "./course_data";
import exam from "./exam_data";
import classs from "./class_data";
import exambatch from "./exambatch_data";
import customer from "./customer_data";
import lesson from "./lesson_data";
import tests from "./avaible_data";
import cus_classs from "./customer_class_data";
import cus_exame from "./customer_exam_date";
import bill_cus_classs from "./bill_cus_class";
import bill_cus_exam from "./Bill_cus_exam";
import Clipboard from "./clipboard_exam";
export default createStore({
  namespaced: true,
  modules: {
    test: import("@/store/avaible_data"),
    acount: acount,
    branch: branch,
    languages: languages,
    certificate: certificate,
    content_exame: content_exame,
    course: course,
    exam: exam,
    class: classs,
    exambatch: exambatch,
    customer: customer,
    lesson: lesson,
    tests: tests,
    cus_class: cus_classs,
    cus_exam: cus_exame,
    bill_cus_class: bill_cus_classs,
    bill_cus_exam: bill_cus_exam,
    clipboard: Clipboard,
  },
});
