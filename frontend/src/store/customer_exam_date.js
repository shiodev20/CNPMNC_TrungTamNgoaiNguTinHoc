import axios from "axios";
const cus_exame = {
  namespaced: true,
  state: {
    cus_exam: null,
    status_chose: 1,
  },
  mutations: {
    set_value: (state, value) => {
      state.cus_exam = value;
    },
  },
  getters: {
    getbyid0: (state) => (id) => {
      return state.cus_exam.filter((e) => e.id == id)[0];
    },
    getbyid: (state) => (idcus, idexam) => {
      return state.cus_exam.filter(
        (e) => e.customer_id == idcus && e.exam_session_id == idexam
      )[0];
    },
    getbyidcus: (state) => (idcus) => {
      return state.cus_exam.filter((e) => e.exam_session_id == idcus);
    },
    getbyidexam: (state) => (idexam) => {
      return state.cus_exam.filter(
        (e) => e.exam_session_id == idexam && e.status_pay == true
      );
    },
    getbyidexam_payfalse: (state) => (idexam) => {
      return state.cus_exam.filter(
        (e) => e.exam_session_id == idexam && e.status_pay == false
      );
    },

    getbyidexam_notpay: (state) => (idexambatch) => {
      const check_date = (value) => {
        const date = new Date(value);
        date.setDate(Number(date.getDate()) + 2);
        if (date.getTime() <= new Date().getTime()) {
          return false;
        }
        return true;
      };
      return state.cus_exam.filter(
        (e) =>
          e.exam_session_id == idexambatch &&
          e.status_pay == false &&
          check_date(e.date_create) == false
      );
    },
  },
  actions: {
    async updatesx({ commit }, datass) {
      console.log(datass);
      await axios.put(
        "http://localhost:3000/customer_exam/" + datass.id,
        datass
      );
      const data = await axios.get("http://localhost:3000/customer_exam");
      commit("set_value", data.data);
    },

    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/customer_exam");
      commit("set_value", data.data);
    },
    async add_cus({ commit }, data) {
      await axios.post("http://localhost:3000/customer_exam", data);
      const datas = await axios.get("http://localhost:3000/customer_exam");
      commit("set_value", datas.data);
    },
    async deleteid({ commit }, id) {
      console.log(commit);
      await axios
        .delete("http://localhost:3000/customer_exam/" + id)
        .catch(() => {
          console.log("Mihequifh");
        });
      const data = await axios.get("http://localhost:3000/customer_exam");
      commit("set_value", data.data);
    },
    async delete({ commit }, id) {
      console.log(commit);
      await axios
        .delete("http://localhost:3000/customer_exam/" + id)
        .catch(() => {
          console.log("Mihequifh");
        });
    },
    async update_ettent({ commit, state }, data) {
      console.log(commit);
      console.log(state);
      await axios.put("http://localhost:3000/customer_exam/" + data.id, data);
    },
  },
};
export default cus_exame;
